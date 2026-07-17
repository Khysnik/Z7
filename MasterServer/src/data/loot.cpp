#include "data/loot.hpp"
#include "data/inventory.hpp"
#include "utils/logger.hpp"
#include "utils/json.hpp"

#include <nlohmann/json.hpp>

#include <algorithm>
#include <fstream>
#include <random>
#include <unordered_set>

namespace gw2::data {

namespace {

using nlohmann::json;

struct Consumable { std::string grants; int64_t qty; std::string rarity; std::string card; std::string team; };
struct Cosmetic   { std::string key; std::string rarity; std::string team; };
struct StickerSet { std::string variant; std::string rarity; std::string unlock; std::string team; std::vector<std::string> pieces; };

struct Tables {
    std::vector<Consumable>  consumables;
    std::vector<Cosmetic>    cosmetics;
    std::vector<StickerSet>  stickers;
    std::vector<std::string> decorations;   // hub/backyard customization keys (hubcustp*)
    std::map<std::string, std::unordered_set<std::string>> cosmeticPools;  // named Cus-key allow-lists
    json packs;               // pack_tables.json "packs" object
    bool loaded = false;
};

// Rarity ranking and weights for random selection
int rarityRank(const std::string& r) {
    if (r == "Common")    return 0;
    if (r == "Uncommon" || r == "Classic") return 1;
    if (r == "Rare" || r == "Special")     return 2;
    if (r == "SuperRare") return 3;
    if (r == "Legendary") return 4;
    return 0;
}

// Probablity table for the different rarities (cosmetics / stickers). Steep curve:
// Legendary is very rare here (matches the character-variant reveal rates).
int rarityWeight(const std::string& r) {
    switch (rarityRank(r)) {
        case 0: return 60;
        case 1: return 25;
        case 2: return 10;
        case 3: return 4;
        default: return 1;
    }
}

// Consumable rarity curve — FLATTER than the cosmetic one. Calibrated from a live
// Minions Booster Pack dump (70 opens / 350 drops) against the actual pool composition
// so a 5x-consumable roll reproduces the observed shares: Common 43% / Uncommon 38% /
// Rare 15% / SuperRare 3%. Special tracks Rare (same rank); Legendary sits below SuperRare.
int consumableRarityWeight(const std::string& r) {
    if (r == "Common")    return 60;
    if (r == "Uncommon")  return 37;
    if (r == "Rare")      return 26;
    if (r == "Special")   return 26;
    if (r == "SuperRare") return 9;
    if (r == "Legendary") return 4;
    return 37;   // default ~Uncommon
}

// Load all loot tables from json files
Tables& tables() {
    static Tables t;
    if (t.loaded) return t;
    t.loaded = true;

    const json& items = utils::dataSection("items");

    // Load consumables
    for (const auto& e : items.value("consumables", json::array()))
        t.consumables.push_back({e.value("grants",""), e.value("qty",(int64_t)1), e.value("rarity",""), e.value("card",""), e.value("team","")});

    // Load cosmetics
    for (const auto& e : items.value("cosmetics", json::array()))
        t.cosmetics.push_back({e.value("key",""), e.value("rarity",""), e.value("team","")});

    // Load sticker sets
    for (const auto& e : items.value("stickers", json::array())) {
        StickerSet st{e.value("variant",""), e.value("rarity",""), e.value("unlock",""), e.value("team",""), {}};
        for (const auto& p : e.value("pieces", json::array())) st.pieces.push_back(p.get<std::string>());
        t.stickers.push_back(std::move(st));
    }

    // Load backyard/hub customization items (what hub chests drop).
    for (const auto& e : utils::dataSection("decorations").value("decorations", json::array()))
        t.decorations.push_back(e.get<std::string>());

    // Named cosmetic allow-lists (e.g. "infinity" = Scrumptious/Infinite items).
    for (const auto& [name, keys] : utils::dataSection("cosmetic_pools").items()) {
        if (!keys.is_array()) continue;   // skip _comment
        for (const auto& k : keys) t.cosmeticPools[name].insert(k.get<std::string>());
    }

    t.packs = utils::dataSection("pack_tables").value("packs", json::object());

    LOG_INFO("[Loot] pools: {} consumables, {} cosmetics, {} sticker sets",
             t.consumables.size(), t.cosmetics.size(), t.stickers.size());
    return t;
}

std::mt19937& rng() { static std::mt19937 g{std::random_device{}()}; return g; }

// True if the player already owns this unlock
bool alreadyHas(const std::string& key, const LootResult& out) {
    for (const auto& u : out.unlocks) if (u == key) return true;
    for (const auto& u : data::getInventoryUnlocks()) if (u == key) return true;
    return false;
}

// When the player owns every piece of a sticker set, grant the character-variant license
void grantVariantIfComplete(LootResult& out, const StickerSet& set) {
    if (set.unlock.empty() || alreadyHas(set.unlock, out)) return;
    if (set.pieces.empty()) return;
    for (const auto& p : set.pieces) if (!alreadyHas(p, out)) return;  // not complete yet
    out.unlocks.push_back(set.unlock);
}

// Roll from a rarity table. weightFn selects the probability curve (cosmetic vs
// consumable) so the two pools can be tuned independently.
template <class T, class Pred>
const T* pickWeighted(const std::vector<T>& v, Pred ok, int (*weightFn)(const std::string&) = rarityWeight) {
    int total = 0;
    for (auto& e : v) if (ok(e)) total += weightFn(e.rarity);
    if (total <= 0) return nullptr;
    int r = std::uniform_int_distribution<int>(0, total - 1)(rng());
    for (auto& e : v) if (ok(e)) { r -= weightFn(e.rarity); if (r < 0) return &e; }
    return nullptr;
}

// Currencies (Coinz / Star / RainbowStar) are not general consumables — they never
// drop from the generic consumable pool. Specific packs that grant coins do so through
// their own slot, not this roll.
bool isCurrencyGrant(const std::string& g) {
    return g == "Coinz" || g == "Star" || g == "RainbowStar";
}

// team filter: "" = no restriction; a team-less (neutral) item is always allowed.
bool teamOk(const std::string& itemTeam, const std::string& want) {
    return want.empty() || itemTeam.empty() || itemTeam == want;
}

// Roll a consumable drop (uses the flatter consumable rarity curve). team restricts to
// a side's spawnables (plant/zombie packs); neutral items (self/team revive) always pass.
void rollConsumable(LootResult& out, int minRank, const std::string& team = "") {
    auto* c = pickWeighted(tables().consumables,
        [&](const Consumable& e){ return !isCurrencyGrant(e.grants) && teamOk(e.team, team) && rarityRank(e.rarity) >= minRank; },
        consumableRarityWeight);
    if (!c) c = pickWeighted(tables().consumables,
        [&](const Consumable& e){ return !isCurrencyGrant(e.grants) && teamOk(e.team, team); }, consumableRarityWeight);
    if (!c) return;
    out.consumables[c->grants] += c->qty;
    out.itli.push_back(c->card.empty() ? c->grants : c->card);  // reveal uses the cnsm card key
}

// Roll a cosmetic drop. Classic-rarity cosmetics only drop when allowClassic is set —
// i.e. from Rux (curated, not rolled) and community-portal gift packs; every normal
// store pack / chest excludes Classic.
void rollCosmetic(LootResult& out, int minRank, bool allowClassic = false, const std::string& team = "",
                  const std::unordered_set<std::string>* pool = nullptr) {
    auto usable = [&](const Cosmetic& e){ return !alreadyHas(e.key, out) && teamOk(e.team, team)
                                              && (!pool || pool->count(e.key)) && (allowClassic || e.rarity != "Classic"); };
    auto* c = pickWeighted(tables().cosmetics, [&](const Cosmetic& e){ return rarityRank(e.rarity) >= minRank && usable(e); });
    if (!c) c = pickWeighted(tables().cosmetics, usable);
    if (!c) { rollConsumable(out, minRank, team); return; }   // all cosmetics owned -> grant a consumable instead
    out.unlocks.push_back(c->key);
    out.itli.push_back(c->key);
}

// Roll an item drop. Character cosmetics are far more common than consumables (~70/30);
// team restricts to one side, pool to a named cosmetic allow-list. (Ratio to be tuned.)
void rollItem(LootResult& out, int minRank, bool allowClassic = false, const std::string& team = "",
              const std::unordered_set<std::string>* pool = nullptr) {
    if (!pool && std::uniform_int_distribution<int>(0, 99)(rng()) < 30) rollConsumable(out, minRank, team);
    else                                                                rollCosmetic(out, minRank, allowClassic, team, pool);
}

// A sticker set passes the slot's team/sets restriction. team "" = any; sets empty = any.
bool stickerAllowed(const StickerSet& s, const std::string& team, const std::vector<std::string>& sets) {
    if (!team.empty() && s.team != team) return false;
    if (!sets.empty() && std::find(sets.begin(), sets.end(), s.unlock) == sets.end()) return false;
    return true;
}

// Roll a sticker piece drop, avoiding dupes and checking variant completion. team/sets
// restrict which character sets are eligible (plant/zombie packs, DLC character packs).
bool rollStickerPiece(LootResult& out, const std::string& rarity,
                      const std::string& team = "", const std::vector<std::string>& sets = {}) {
    // Pick a set of this rarity that still has an unowned piece, grant one piece, and complete the variant if it was the last missing piece.
    std::vector<const StickerSet*> cand;
    for (auto& s : tables().stickers) {
        if (!rarity.empty() && s.rarity != rarity) continue;
        if (!stickerAllowed(s, team, sets)) continue;
        for (const auto& p : s.pieces) if (!alreadyHas(p, out)) { cand.push_back(&s); break; }
    }
    if (cand.empty()) return false;
    const StickerSet* set = cand[std::uniform_int_distribution<size_t>(0, cand.size()-1)(rng())];

    std::vector<std::string> avail;
    for (const auto& p : set->pieces) if (!alreadyHas(p, out)) avail.push_back(p);
    const std::string& piece = avail[std::uniform_int_distribution<size_t>(0, avail.size()-1)(rng())];
    out.unlocks.push_back(piece);
    out.itli.push_back(piece);
    grantVariantIfComplete(out, *set);
    return true;
}

// Roll a full sticker set
void rollStickerSet(LootResult& out, int minRank, int maxRank, bool excludeLegendary,
                    const std::string& team = "", const std::vector<std::string>& sets = {}) {
    std::vector<const StickerSet*> cand;
    for (auto& s : tables().stickers) {
        int rk = rarityRank(s.rarity);
        if (rk < minRank || rk > maxRank) continue;
        if (excludeLegendary && s.rarity == "Legendary") continue;
        if (!stickerAllowed(s, team, sets)) continue;
        if (s.pieces.size() < 5) continue;          // full 5-piece sets only
        // Skip characters the player already owns
        bool fullyOwned = true;
        for (auto& p : s.pieces) if (!alreadyHas(p, out)) { fullyOwned = false; break; }
        if (fullyOwned) continue;
        cand.push_back(&s);
    }
    if (cand.empty()) return;
    const StickerSet* set = cand[std::uniform_int_distribution<size_t>(0, cand.size()-1)(rng())];
    for (auto& p : set->pieces) {
        if (alreadyHas(p, out)) continue;           // don't regrant pieces already owned
        out.unlocks.push_back(p);
        out.itli.push_back(p);
    }
    grantVariantIfComplete(out, *set);              // full set -> unlock the variant
}

// Run a single pack roll, which may grant a consumable, cosmetic, item, or sticker piece/set
void runSlot(LootResult& out, const json& slot, bool allowClassic = false) {
    std::string source = slot.value("source", "");
    int minRank = slot.contains("minRarity") ? rarityRank(slot["minRarity"].get<std::string>()) : 0;
    // Optional restrictions: team ("plant"/"zombie") and a set-unlock allow-list.
    const std::string team = slot.value("team", std::string());
    std::vector<std::string> sets;
    for (const auto& s : slot.value("sets", json::array())) sets.push_back(s.get<std::string>());
    // Named cosmetic allow-list ("pool"), e.g. the Infinity pack's Scrumptious/Infinite set.
    const std::unordered_set<std::string>* pool = nullptr;
    if (slot.contains("pool")) {
        auto it = tables().cosmeticPools.find(slot["pool"].get<std::string>());
        if (it != tables().cosmeticPools.end()) pool = &it->second;
    }

    if (source == "consumable")    rollConsumable(out, minRank, team);
    else if (source == "cosmetic") rollCosmetic(out, minRank, allowClassic, team, pool);
    else if (source == "item")     rollItem(out, minRank, allowClassic, team, pool);
    else if (source == "sticker") {
        double chance = slot.value("chance", 1.0);
        bool hit = std::uniform_real_distribution<double>(0, 1)(rng()) < chance;
        if (!hit || !rollStickerPiece(out, slot.value("rarity", ""), team, sets)) {
            std::string els = slot.value("else", "item");
            if (els == "consumable") rollConsumable(out, minRank, team);
            else if (els == "cosmetic") rollCosmetic(out, minRank, allowClassic, team, pool);
            else rollItem(out, minRank, allowClassic, team, pool);
        }
    } else if (source == "stickerSet") {
        int lo = 99, hi = 0;
        for (const auto& r : slot.value("rarityIn", json::array())) { int rk = rarityRank(r.get<std::string>()); lo = std::min(lo, rk); hi = std::max(hi, rk); }
        if (lo > hi) { lo = 2; hi = 3; }
        rollStickerSet(out, lo, hi, slot.value("excludeLegendary", true), team, sets);
    }
}

} // namespace

// Get the cost of a pack by its key, or -1 if the pack doesn't exist
int64_t packCost(const std::string& pkey) {
    const json& packs = tables().packs;
    auto it = packs.find(pkey);
    if (it == packs.end()) return -1;
    return it->value("cost", (int64_t)0);
}

bool packHasReward(const std::string& pkey) {
    const json& packs = tables().packs;
    auto it = packs.find(pkey);
    if (it == packs.end()) return false;                 // unknown pack

    bool sawCharSlot = false;
    const auto& unlocks = getInventoryUnlocks();
    std::unordered_set<std::string> owned(unlocks.begin(), unlocks.end());

    for (const auto& slot : it->value("slots", json::array())) {
        if (slot.value("source", "") != "stickerSet") continue;
        sawCharSlot = true;
        int lo = 99, hi = 0;
        for (const auto& r : slot.value("rarityIn", json::array())) {
            int rk = rarityRank(r.get<std::string>()); lo = std::min(lo, rk); hi = std::max(hi, rk);
        }
        if (lo > hi) { lo = 2; hi = 3; }
        const bool excludeLegendary = slot.value("excludeLegendary", true);
        for (const auto& s : tables().stickers) {
            const int rk = rarityRank(s.rarity);
            if (rk < lo || rk > hi) continue;
            if (excludeLegendary && s.rarity == "Legendary") continue;
            if (s.pieces.size() < 5) continue;
            for (const auto& p : s.pieces)
                if (!owned.count(p)) return true;        // an unowned piece remains
        }
    }
    return !sawCharSlot;   // char pack with everything owned -> no reward
}

LootResult rollChest() {
    LootResult out;
    out.valid = true;
    rollCosmetic(out, 0);
    return out;
}

// A hub/backyard chest: drops a backyard customization item, with a small chance of a
// coin drop (50k) instead. Verified from a live capture (largePackDump).
LootResult rollHubChest() {
    LootResult out;
    out.valid = true;
    if (std::uniform_int_distribution<int>(0, 99)(rng()) < 10) {   // small chance -> coins
        out.consumables["Coinz"] += 50000;
        out.itli.push_back("cnsm_coin50");
        return out;
    }
    std::vector<const std::string*> avail;
    for (const auto& dc : tables().decorations) if (!alreadyHas(dc, out)) avail.push_back(&dc);
    if (avail.empty()) { rollCosmetic(out, 0); return out; }       // all backyard items owned
    const std::string& dc = *avail[std::uniform_int_distribution<size_t>(0, avail.size()-1)(rng())];
    out.unlocks.push_back(dc);
    out.itli.push_back(dc);
    return out;
}

LootResult rollCosmeticPack(int count) {
    LootResult out;
    out.valid = true;
    // rollCosmetic avoids dupes within a single result (alreadyHas checks out.unlocks),
    // and falls back to a consumable when every cosmetic is owned. Portal gift packs are
    // one of the two Classic-allowed sources (the other is Rux's curated list).
    for (int i = 0; i < std::max(1, count); ++i) rollCosmetic(out, 0, /*allowClassic*/ true);
    return out;
}

LootResult rollFixedPack(const std::string& pkey) {
    LootResult out;
    const json& fp = utils::dataSection("fixed_packs");
    auto it = fp.find(pkey);
    if (it == fp.end() || !it->is_array()) return out;   // not a fixed pack -> invalid
    out.valid = true;
    for (const auto& e : *it) {
        const std::string card   = e.value("card", std::string());
        const std::string grants = e.value("grants", std::string());
        const int64_t     qty    = e.value("qty", (int64_t)1);
        const bool        reveal = e.value("reveal", true);   // false = grant silently (e.g. a variant license)
        if (!grants.empty())      out.consumables[grants] += qty;   // currency / consumable
        else if (!card.empty())   out.unlocks.push_back(card);      // unlock (emoji / customization / ability)
        if (!card.empty() && reveal) out.itli.push_back(card);      // reveal card key
    }
    return out;
}

// Roll a pack by its keyname, returning invalid if the pack doesnt exist
LootResult rollPack(const std::string& pkey) {
    LootResult out;
    const json& packs = tables().packs;
    auto it = packs.find(pkey);
    if (it == packs.end()) return out;            // unknown pack -> invalid

    const json& pack = *it;
    out.valid = true;
    out.name  = pack.value("name", "");
    out.cost  = pack.value("cost", (int64_t)0);

    for (const auto& slot : pack.value("slots", json::array())) {
        int repeat = slot.value("repeat", 1);
        for (int k = 0; k < repeat; ++k) runSlot(out, slot);
    }
    return out;
}

int reconcileVariants() {
    const auto& unlocks = getInventoryUnlocks();
    std::unordered_set<std::string> owned(unlocks.begin(), unlocks.end());
    int granted = 0;
    for (const auto& s : tables().stickers) {
        if (s.unlock.empty() || s.pieces.empty() || owned.count(s.unlock)) continue;
        bool complete = true;
        for (const auto& p : s.pieces) if (!owned.count(p)) { complete = false; break; }
        if (complete) { addInventoryUnlock(s.unlock); owned.insert(s.unlock); ++granted; }
    }
    return granted;
}

} // namespace gw2::data
