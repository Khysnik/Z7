<script>
    import { Command } from "@tauri-apps/plugin-shell";
    import { invoke } from "@tauri-apps/api/core";
    import { open as openDialog, message } from "@tauri-apps/plugin-dialog";
    import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
    import { baseDir, dataDir } from "$lib/paths.js";
    import {
        onlineMode,
        onlineServer,
        communityChallengeEnabled,
        communityPortalEnabled,
    } from "$lib/state.js";

    const GAME_EXE = "GW2.Main_Win64_Retail.exe";
    const PATCHED_EXE = "GW2-Z7.exe";
    const HOSTS = "C:\\Windows\\System32\\drivers\\etc\\hosts";
    const REDIRECT_IP = "127.0.0.1";
    const REDIRECT_HOST = "winter15.gosredirector.ea.com";

    let gameDir = $state(""); // chosen game folder
    let patched = $state(false); // GW2-Z7.exe present?
    let busy = $state(false);
    let log = $state([]);
    const say = (m) => (log = [...log, m]);

    // Player username — persisted to data/config.json's "persona" field, which the
    // Blaze server serves as the account's display name.
    let persona = $state("");
    let personaSaved = $state(false);
    let cfgPath = "";

    async function loadPersona() {
        try {
            cfgPath = `${await dataDir()}\\config.json`;
            const c = JSON.parse(await readTextFile(cfgPath));
            persona = c.persona ?? "";
        } catch (e) {
            say(`Couldn't read username: ${e}`);
        }
    }

    async function savePersona() {
        const name = persona.trim();
        if (!cfgPath || !name) return;
        try {
            const c = JSON.parse(await readTextFile(cfgPath));
            if (c.persona === name) return;
            c.persona = name;
            await writeTextFile(cfgPath, JSON.stringify(c, null, 1));
            personaSaved = true;
            setTimeout(() => (personaSaved = false), 1500);
        } catch (e) {
            say(`Couldn't save username: ${e}`);
        }
    }

    loadPersona();

    // restore last chosen folder
    if (typeof localStorage !== "undefined") {
        gameDir = localStorage.getItem("gw2dir") ?? "";
        if (gameDir) refreshPatched();
    }

    const cmd = (args) => Command.create("cmd", args).execute();
    // Fire-and-forget: don't await (the launched process holds the stdio pipe,
    // so execute() wouldn't resolve until it exits). cmd has already detached
    // the target via `start` by the time that matters.
    const fire = (args) =>
        Command.create("cmd", args)
            .execute()
            .catch((e) => say(`launch warning: ${e}`));
    const dirOf = (p) =>
        p.slice(0, Math.max(p.lastIndexOf("\\"), p.lastIndexOf("/")));

    async function exists(path) {
        const r = await cmd(["/c", "if", "exist", path, "echo", "YES"]);
        return r.stdout.includes("YES");
    }
    async function refreshPatched() {
        patched = gameDir ? await exists(`${gameDir}\\${PATCHED_EXE}`) : false;
    }

    // The account identity (blazeId/PID + nucleusId), generated once per install and
    // stored in data/.pid. blazeId is 20138 + 8 digits, nucleusId is 20295 + 8 digits,
    // matching the id ranges the game expects. Passed to gw2-server as -pid / -nid.
    async function ensurePid() {
        const pidPath = `${await dataDir()}\\.pid`;
        try {
            const p = JSON.parse(await readTextFile(pidPath));
            if (p.blazeId && p.nucleusId) return p;
        } catch {
            // no .pid yet (first launch) — fall through and generate one
        }
        const rand8 = () => Math.floor(Math.random() * 1e8); // 0 .. 99,999,999
        const ids = {
            blazeId: 20138 * 1e8 + rand8(),
            nucleusId: 20295 * 1e8 + rand8(),
        };
        try {
            await writeTextFile(pidPath, JSON.stringify(ids, null, 1));
            say(`Generated player ID ${ids.blazeId}.`);
        } catch (e) {
            say(`Couldn't save player ID: ${e}`);
        }
        return ids;
    }

    // Start a server hidden in the background only if it isn't already running.
    async function ensureServer(exe, args, cwd, note) {
        if (await invoke("proc_running", { name: exe })) {
            say(`${exe} already running.`);
            return;
        }
        say(`Starting ${exe}${note ? ` (${note})` : ""}…`);
        try {
            await invoke("spawn_hidden", { program: `${cwd}\\${exe}`, args, cwd });
        } catch (e) {
            say(`Failed to start ${exe}: ${e}`);
        }
    }

    async function choose() {
        const file = await openDialog({
            multiple: false,
            directory: false,
            title: "Select your GW2 game .exe",
            filters: [{ name: "Game executable", extensions: ["exe"] }],
        });
        if (!file) return;
        gameDir = dirOf(file);
        localStorage.setItem("gw2dir", gameDir);
        log = [];
        await refreshPatched();
    }

    async function hostsOk() {
        const txt = await readTextFile(HOSTS);
        return txt.split(/\r?\n/).some((line) => {
            const l = line.split("#")[0].trim();
            if (!l) return false;
            const parts = l.split(/\s+/);
            return (
                parts[0] === REDIRECT_IP &&
                parts.slice(1).includes(REDIRECT_HOST)
            );
        });
    }

    async function patch() {
        const Z7 = await baseDir();
        const orig = `${gameDir}\\${GAME_EXE}`;
        const out = `${gameDir}\\${PATCHED_EXE}`;
        const tmp = `${gameDir}\\_gw2_eaac.tmp`;
        const courgette = `${Z7}\\courgette.exe`;
        if (!(await exists(orig)))
            throw new Error(`${GAME_EXE} not found in ${gameDir}`);

        await cmd(["/c", "del", "/q", tmp]);

        say("Applying PreEAAC patch…");
        // run courgette via cmd so the program path needn't be a fixed scope entry
        let r = await cmd([
            "/c",
            courgette,
            "-apply",
            orig,
            `${Z7}\\PreEAAC.patch`,
            tmp,
        ]);
        if (r.code !== 0)
            throw new Error(`EAAC patch failed: ${r.stderr || r.code}`);

        say("Applying CertPin patch…");
        r = await cmd([
            "/c",
            courgette,
            "-applybsdiff",
            tmp,
            `${Z7}\\CertPin.patch`,
            out,
        ]);
        if (r.code !== 0)
            throw new Error(`Cert-pin patch failed: ${r.stderr || r.code}`);

        await cmd(["/c", "del", "/q", tmp]);
        say(`Patched → ${PATCHED_EXE}`);
    }

    async function launch() {
        if (!gameDir) return;
        busy = true;
        log = [];
        try {
            if (!(await hostsOk())) {
                await message(
                    `Your hosts file is missing the redirect entry. The game can't connect without it.\n\n` +
                        `Add this line to ${HOSTS} using Notepad run as Administrator, then try again:\n\n` +
                        `${REDIRECT_IP} ${REDIRECT_HOST}`,
                    { title: "Missing hosts entry", kind: "error" },
                );
                return;
            }

            if (!(await exists(`${gameDir}\\${PATCHED_EXE}`))) {
                say("Patching game…");
                await patch();
            } else {
                say(`${PATCHED_EXE} found — skipping patch.`);
            }

            const Z7 = await baseDir();

            // BlazeServer is always local (bytevault, redirector, etc). gw2-server
            // gets "-online <addr>" when Online Mode routes editorial at the shared
            // server. Only the servers that aren't already running get started, and
            // both run hidden in the background (no console windows).
            await ensureServer("BlazeServer.exe", [], Z7);

            const ids = await ensurePid();
            const gargs = [];
            if ($onlineMode) gargs.push("-online", $onlineServer);
            gargs.push("-pid", String(ids.blazeId), "-nid", String(ids.nucleusId));
            if (persona.trim()) gargs.push("-name", persona.trim());
            // Offline mode only: disabled community features tell the server to reply empty.
            if (!$onlineMode) {
                if (!$communityChallengeEnabled) gargs.push("-disableCC");
                if (!$communityPortalEnabled) gargs.push("-disableCP");
            }
            await ensureServer(
                "gw2-server.exe",
                gargs,
                Z7,
                $onlineMode ? `online ${$onlineServer}` : "",
            );

            say("Launching game…");
            fire(["/c", "start", "", "/d", gameDir, PATCHED_EXE]);
            say("Launched. Both servers will close when the game exits.");

            // When the game (GW2-Z7.exe) is closed, shut down both servers.
            invoke("watch_and_cleanup", {
                watch: PATCHED_EXE,
                kill: ["gw2-server.exe", "BlazeServer.exe"],
            });
            await refreshPatched();
        } catch (e) {
            say(`Error: ${e}`);
            await message(String(e), { title: "Launch failed", kind: "error" });
        } finally {
            busy = false;
        }
    }
</script>

<svelte:head>
    <title>Z7 Launcher</title>
</svelte:head>

<div class="container">
    <div>
        <h1>Z7 Launcher</h1>
    </div>

    <div class="card">
        <h2>Player</h2>
        <div class="stack" style="margin-top: 12px;">
            <label class="card-desc" for="persona" style="margin: 0;">Username</label>
            <input
                id="persona"
                type="text"
                bind:value={persona}
                onblur={savePersona}
                placeholder="Enter a username"
                spellcheck="false"
            />
            {#if personaSaved}
                <span class="card-desc" style="color: var(--green, #16a34a);">Saved.</span>
            {/if}
        </div>
    </div>

    <div class="card">
        <div class="row" style="justify-content: space-between;">
            <div>
                <h2>Online Mode</h2>
            </div>
            <label class="switch">
                <input type="checkbox" bind:checked={$onlineMode} />
                <span class="slider"></span>
            </label>
        </div>

        {#if $onlineMode}
            <div class="stack" style="margin-top: 12px;">
                <label class="card-desc" for="onlinesrv" style="margin: 0;">Server address (ip:port)</label>
                <input
                    id="onlinesrv"
                    type="text"
                    bind:value={$onlineServer}
                    placeholder="208.117.82.46:42220"
                    spellcheck="false"
                />
            </div>
        {/if}
    </div>

    <div class="card">
        <h2>Launch Game</h2>
<br>
        <div class="stack">
            <div class="row">
                <button class="btn outline" onclick={choose}>Choose game .exe</button>
                {#if gameDir}
                    <span class="path mono">{gameDir}</span>
                {/if}
            </div>

            <button class="btn launch lg" disabled={!gameDir || busy} onclick={launch}>
                {busy ? "Working…" : "Launch"}
            </button>
        </div>
    </div>

    {#if log.length}
        <div class="card">
            <h2>Log</h2>
            <pre class="log">{log.join("\n")}</pre>
        </div>
    {/if}
</div>
