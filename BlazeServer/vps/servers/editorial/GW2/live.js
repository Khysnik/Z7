import fs from "node:fs";
import path from "node:path";
import { serverDir } from "../../../utils/appdir.js";

const liveDir = path.join(serverDir(import.meta.url, "editorial/GW2"), "live");
const readJson = (f) => JSON.parse(fs.readFileSync(path.join(liveDir, f), "utf-8"));

function mulberry32(seed) {
    let a = seed >>> 0;
    return function () {
        a = (a + 0x6d2b79f5) | 0;
        let t = Math.imul(a ^ (a >>> 15), 1 | a);
        t = (t + Math.imul(t ^ (t >>> 7), 61 | t)) ^ t;
        return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
    };
}
function hashStr(s) {
    let h = 2166136261 >>> 0;
    for (let i = 0; i < s.length; i++) { h ^= s.charCodeAt(i); h = Math.imul(h, 16777619); }
    return h >>> 0;
}
const nowUnix = () => Math.floor(Date.now() / 1000);
const rotationIndex = (base, period, now) => Math.floor((now - base) / period);

export function blackmarket(now = nowUnix()) {
    const cfg = readJson("blackmarket.json");
    const base = cfg.rotationBaseUnix, period = cfg.rotationPeriodSecs;
    const index = rotationIndex(base, period, now);
    const activationUnix = base + index * period;
    const stateChangeTimeUnix = activationUnix + period;

    const tiers = cfg.tiers.slice();
    const orderRng = mulberry32((index * 2654435761) >>> 0);
    for (let i = tiers.length - 1; i > 0; i--) {
        const j = Math.floor(orderRng() * (i + 1));
        [tiers[i], tiers[j]] = [tiers[j], tiers[i]];
    }

    const count = Math.min(cfg.slots, tiers.length);
    const round = cfg.priceRoundTo > 0 ? cfg.priceRoundTo : 1;
    const slots = [];
    for (let i = 0; i < count; i++) {
        const t = tiers[i];
        const rng = mulberry32((index ^ hashStr(t.id)) >>> 0);
        const itid = t.candidates[Math.floor(rng() * t.candidates.length)];
        let price;
        if (t.price && t.price > 0) {
            price = t.price;
        } else {
            const span = Math.floor((t.priceMax - t.priceMin) / round) + 1;
            price = t.priceMin + Math.floor(rng() * span) * round;
        }
        slots.push({ kind: t.kind, itid, price });
    }
    slots.sort((a, b) => b.price - a.price);
    slots.forEach((s, i) => { s.slid = "z7bm" + (i + 1); s.name = ""; s.desc = ""; });

    return {
        acid: cfg.acid,
        ownedLicenses: cfg.ownedLicenses,
        haveItemsDialog: cfg.haveItemsDialog,
        noItemsDialog: cfg.noItemsDialog,
        stillDecidingDialog: cfg.stillDecidingDialog,
        rotationIndex: index,
        activationUnix,
        stateChangeTimeUnix,
        slots,
    };
}

export function dailyQuests(now = nowUnix()) {
    const cfg = readJson("dailyquests.json");
    const base = cfg.rotationBaseUnix, period = cfg.rotationPeriodSecs;
    const index = rotationIndex(base, period, now);
    const activationUnix = base + index * period;
    const expiryUnix = activationUnix + period;

    const pick = (pool, category) => {
        const arr = (pool || []).slice();
        const rng = mulberry32((index ^ hashStr(category)) >>> 0);
        for (let i = arr.length - 1; i > 0; i--) {
            const j = Math.floor(rng() * (i + 1));
            [arr[i], arr[j]] = [arr[j], arr[i]];
        }
        return arr.slice(0, Math.min(cfg.pickPerCategory, arr.length));
    };

    const quests = [
        ...pick(cfg.multiplayer, "multiplayer"),
        ...pick(cfg.plant, "plant"),
        ...pick(cfg.zombie, "zombie"),
    ];

    return { quests, activationUnix, expiryUnix, rotationIndex: index };
}

export function communityEvent(now = nowUnix()) {
    const ev = readJson("communityevent.json");
    const base = ev.rotationBaseUnix, period = ev.rotationPeriodSecs;
    const index = rotationIndex(base, period, now);
    const startUnix = base + index * period;
    const endUnix = startUnix + period;
    return {
        featured: !!ev.featured,
        eventId: ev.eventId,
        name: ev.name,
        description: ev.description,
        imageUrl: ev.imageUrl,
        startUnix,
        endUnix,
        grandPrizeEndUnix: endUnix,
        firstChestScore: ev.firstChestScore,
        secondChestScore: ev.secondChestScore,
        thirdChestScore: ev.thirdChestScore,
        chestReward: ev.chestReward,
        eventReward: ev.eventReward,
        rotationIndex: index,
    };
}

const challengeStatePath = path.join(liveDir, "challenge_state.json");
function readChallengeState() {
    try { return JSON.parse(fs.readFileSync(challengeStatePath, "utf-8")); }
    catch { return { window: -1, community: 0, users: {} }; }
}
function writeChallengeState(s) {
    fs.writeFileSync(challengeStatePath, JSON.stringify(s, null, 1));
}

export function contributeChallenge(body, now = nowUnix()) {
    const c = readJson("communitychallenge.json");
    const index = rotationIndex(c.rotationBaseUnix, c.rotationPeriodSecs, now);
    let st = readChallengeState();
    if (st.window !== index) st = { window: index, community: 0, users: {} };

    const tracks = c.tracks || { type: "elements", keys: [] };
    const bucket = (body && body[tracks.type]) || {};
    let matched = 0;
    for (const k of tracks.keys) matched += Number(bucket[k] || 0);

    if (matched > 0) {
        st.community += matched;
        const u = String((body && body.user) || "0");
        st.users[u] = (st.users[u] || 0) + matched;
        writeChallengeState(st);
    }
    return { matched, community: st.community, window: index };
}

const NO_TIME = 1000000;
const leaderboardStatePath = path.join(liveDir, "leaderboard_state.json");
function readLeaderboardState() {
    try { return JSON.parse(fs.readFileSync(leaderboardStatePath, "utf-8")); }
    catch { return { boards: {} }; }
}
function writeLeaderboardState(s) {
    fs.writeFileSync(leaderboardStatePath, JSON.stringify(s, null, 1));
}

export function submitLeaderboard(body) {
    if (!body || body.user == null || !body.scores) return { ok: false };
    const st = readLeaderboardState();
    const user = String(body.user);
    const name = body.name || ("Player" + user.slice(-4));
    let updated = 0;
    for (const [board, s] of Object.entries(body.scores)) {
        const value = Number(s && s.value);
        if (!isFinite(value) || value >= NO_TIME) continue;
        st.boards[board] = st.boards[board] || {};
        const prev = st.boards[board][user];
        if (!prev || value < prev.value) {
            st.boards[board][user] = { value, character: Number((s && s.character) || 0), name };
            updated++;
        } else {
            prev.name = name;
        }
    }
    writeLeaderboardState(st);
    return { ok: true, updated };
}

export function leaderboard(board, limit = 100) {
    const st = readLeaderboardState();
    const tbl = st.boards[board] || {};
    const entries = Object.entries(tbl)
        .map(([user, e]) => ({ user, name: e.name, value: e.value, character: e.character }))
        .filter(e => e.value < NO_TIME)
        .sort((a, b) => a.value - b.value)
        .slice(0, Math.max(1, limit));
    entries.forEach((e, i) => { e.rank = i + 1; });
    return { board, size: entries.length, entries };
}

export function communityChallenge(now = nowUnix(), user = null) {
    const c = readJson("communitychallenge.json");
    const base = c.rotationBaseUnix, period = c.rotationPeriodSecs;
    const index = rotationIndex(base, period, now);
    const startUnix = base + index * period;
    const endUnix = startUnix + period;
    const elapsed = Math.max(0, now - startUnix);

    const st = readChallengeState();
    const community = (st.window === index) ? st.community : 0;
    const userProgress = (st.window === index && user != null) ? (st.users[String(user)] || 0) : 0;

    return {
        achievementId: c.achievementId,
        name: c.name,
        desc: c.desc,
        image: c.image,
        personalHeader: c.personalHeader,
        rewardHeader: c.rewardHeader,
        challengeActive: 1,
        contributionActive: 1,
        communityProgress: community,
        bronzeThreshold: c.bronzeThreshold,
        silverThreshold: c.silverThreshold,
        goldThreshold: c.goldThreshold,
        bronzeReward: c.bronzeReward,
        silverReward: c.silverReward,
        goldReward: c.goldReward,
        bronzeCollected: 0,
        silverCollected: 0,
        goldCollected: 0,
        userThreshold: c.userThreshold,
        userProgress,
        secondsFromStart: elapsed,
        secondsToNextEvent: Math.max(0, endUnix - now),
        secondsToCollectionExpiry: Math.max(0, endUnix - now),
        secondsToRewardExpiry: Math.max(0, endUnix - now),
        refreshRateSeconds: c.refreshRateSeconds,
        proximityCooldownSeconds: c.proximityCooldownSeconds,
        rotationIndex: index,
    };
}
