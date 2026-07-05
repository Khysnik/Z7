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

struct Consumable { std::string grants; int64_t qty; std::string rarity; std::string card; };
struct Cosmetic   { std::string key; std::string rarity; };
struct StickerSet { std::string variant; std::string rarity; std::string unlock; std::vector<std::string> pieces; };

struct Tables {
    std::vector<Consumable> consumables;
    std::vector<Cosmetic>   cosmetics;
    std::vector<StickerSet> stickers;
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

// Probablity table for the different rarities
int rarityWeight(const std::string& r) {
    switch (rarityRank(r)) { 
        case 0: return 60; 
        case 1: return 25; 
        case 2: return 10; 
        case 3: return 4; 
        default: return 1; 
    }
}

// Load all loot tables from json files
Tables& tables() {
    static Tables t;
    if (t.loaded) return t;
    t.loaded = true;

    const json& items = utils::dataSection("items");

    // Load consumables
    for (const auto& e : items.value("consumables", json::array()))
        t.consumables.push_back({e.value("grants",""), e.value("qty",(int64_t)1), e.value("rarity",""), e.value("card","")});

    // Load cosmetics
    for (const auto& e : items.value("cosmetics", json::array()))
        t.cosmetics.push_back({e.value("key",""), e.value("rarity","")});

    // Load sticker sets
    for (const auto& e : items.value("stickers", json::array())) {
        StickerSet st{e.value("variant",""), e.value("rarity",""), e.value("unlock",""), {}};
        for (const auto& p : e.value("pieces", json::array())) st.pieces.push_back(p.get<std::string>());
        t.stickers.push_back(std::move(st));
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

template <class T, class Pred>

// Roll from the rarity table
const T* pickWeighted(const std::vector<T>& v, Pred ok) {
    int total = 0;
    for (auto& e : v) if (ok(e)) total += rarityWeight(e.rarity);
    if (total <= 0) return nullptr;
    int r = std::uniform_int_distribution<int>(0, total - 1)(rng());
    for (auto& e : v) if (ok(e)) { r -= rarityWeight(e.rarity); if (r < 0) return &e; }
    return nullptr;
}

// Roll a consumable drop
void rollConsumable(LootResult& out, int minRank) {
    auto* c = pickWeighted(tables().consumables, [&](const Consumable& e){ return rarityRank(e.rarity) >= minRank; });
    if (!c) c = pickWeighted(tables().consumables, [](const Consumable&){ return true; });
    if (!c) return;
    out.consumables[c->grants] += c->qty;
    out.itli.push_back(c->card.empty() ? c->grants : c->card);  // reveal uses the cnsm card key
}

// Roll a cosmetic drop
void rollCosmetic(LootResult& out, int minRank) {
    auto* c = pickWeighted(tables().cosmetics, [&](const Cosmetic& e){ return rarityRank(e.rarity) >= minRank && !alreadyHas(e.key, out); });
    if (!c) c = pickWeighted(tables().cosmetics, [&](const Cosmetic& e){ return !alreadyHas(e.key, out); });
    if (!c) { rollConsumable(out, minRank); return; }   // all cosmetics owned -> grant a consumable instead
    out.unlocks.push_back(c->key);
    out.itli.push_back(c->key);
}

// Roll an item drop (consumable or cosmetic)
void rollItem(LootResult& out, int minRank) {
    if (std::uniform_int_distribution<int>(0, 1)(rng())) rollConsumable(out, minRank); else rollCosmetic(out, minRank);
}

// Roll a sticker piece drop, avoiding dupes and checking variant completion
bool rollStickerPiece(LootResult& out, const std::string& rarity) {
    // Pick a set of this rarity that still has an unowned piece, grant one piece, and complete the variant if it was the last missing piece.
    std::vector<const StickerSet*> cand;
    for (auto& s : tables().stickers) {
        if (s.rarity != rarity) continue;
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
void rollStickerSet(LootResult& out, int minRank, int maxRank, bool excludeLegendary) {
    std::vector<const StickerSet*> cand;
    for (auto& s : tables().stickers) {
        int rk = rarityRank(s.rarity);
        if (rk < minRank || rk > maxRank) continue;
        if (excludeLegendary && s.rarity == "Legendary") continue;
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
void runSlot(LootResult& out, const json& slot) {
    std::string source = slot.value("source", "");
    int minRank = slot.contains("minRarity") ? rarityRank(slot["minRarity"].get<std::string>()) : 0;

    if (source == "consumable")    rollConsumable(out, minRank);
    else if (source == "cosmetic") rollCosmetic(out, minRank);
    else if (source == "item")     rollItem(out, minRank);
    else if (source == "sticker") {
        double chance = slot.value("chance", 1.0);
        bool hit = std::uniform_real_distribution<double>(0, 1)(rng()) < chance;
        if (!hit || !rollStickerPiece(out, slot.value("rarity", ""))) {
            std::string els = slot.value("else", "item");
            if (els == "consumable") rollConsumable(out, minRank);
            else if (els == "cosmetic") rollCosmetic(out, minRank);
            else rollItem(out, minRank);
        }
    } else if (source == "stickerSet") {
        int lo = 99, hi = 0;
        for (const auto& r : slot.value("rarityIn", json::array())) { int rk = rarityRank(r.get<std::string>()); lo = std::min(lo, rk); hi = std::max(hi, rk); }
        if (lo > hi) { lo = 2; hi = 3; }
        rollStickerSet(out, lo, hi, slot.value("excludeLegendary", true));
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
