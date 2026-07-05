import { writable } from "svelte/store";

// Online Mode: when on, the launcher points gw2-server at a shared/VPS editorial
// server (via "-online <ip>") instead of a local one, and hides the local data
// editors that don't apply online (Black Market, Community Portal, Community
// Challenge). Persisted across launches.
const initial =
    typeof localStorage !== "undefined" && localStorage.getItem("onlineMode") === "1";

export const onlineMode = writable(initial);

if (typeof localStorage !== "undefined") {
    onlineMode.subscribe((v) => localStorage.setItem("onlineMode", v ? "1" : "0"));
}

// Address (ip[:port]) of the shared editorial server, used for the "-online <ip>"
// arg when Online Mode is on. Persisted.
const initialServer =
    (typeof localStorage !== "undefined" && localStorage.getItem("onlineServer")) ||
    "208.117.82.46:42220";

export const onlineServer = writable(initialServer);

if (typeof localStorage !== "undefined") {
    onlineServer.subscribe((v) => localStorage.setItem("onlineServer", v ?? ""));
}

// Offline-mode feature toggles. Default enabled. When Online Mode is OFF and one of
// these is disabled, the launcher starts gw2-server with "-disableCC"/"-disableCP" so
// the Blaze server replies empty to that community query. Persisted across launches.
function persistedBool(key, dflt) {
    const start =
        typeof localStorage !== "undefined" && localStorage.getItem(key) != null
            ? localStorage.getItem(key) === "1"
            : dflt;
    const store = writable(start);
    if (typeof localStorage !== "undefined")
        store.subscribe((v) => localStorage.setItem(key, v ? "1" : "0"));
    return store;
}

export const communityChallengeEnabled = persistedBool("ccEnabled", true);
export const communityPortalEnabled = persistedBool("cpEnabled", true);
