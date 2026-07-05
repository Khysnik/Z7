<script>
    import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
    import * as Card from "$lib/components/ui/card/index.js";
    import { Button } from "$lib/components/ui/button/index.js";
    import { Input } from "$lib/components/ui/input/index.js";
    import { Label } from "$lib/components/ui/label/index.js";
    import { dataDir, launcherDir } from "$lib/paths.js";

    let PATH = "";
    let MAP_PATH = "";
    const CAP = 300;

    const CATS = [
        { id: "achievements", label: "Achievements" },
        { id: "story", label: "Story Missions" },
        { id: "stats", label: "Stats" },
        { id: "hub", label: "Hub" },
        { id: "misc", label: "Misc" },
    ];

    let raw = $state(null);
    let map = $state({}); // key -> { name, cat }
    let vals = $state({}); // key -> number
    let isFloat = {};
    let buckets = $state({ achievements: [], story: [], stats: [], hub: [], misc: [] });
    let activeCat = $state("achievements");
    let query = $state("");
    let error = $state("");
    let saving = $state(false);
    let saved = $state(false);

    async function load() {
        try {
            PATH = `${await dataDir()}\\MPProfile.json`;
            MAP_PATH = `${await launcherDir()}\\stats_map.json`;
            raw = JSON.parse(await readTextFile(PATH));
            map = JSON.parse(await readTextFile(MAP_PATH));
            const stats = raw.stats ?? {};
            const b = { achievements: [], story: [], stats: [], hub: [], misc: [] };
            for (const k of Object.keys(stats)) {
                const s = String(stats[k]);
                isFloat[k] = s.includes(".");
                vals[k] = Number(s);
                // Hub-related stats (display name starts with "Hub") get their own tab.
                if ((map[k]?.name ?? "").toLowerCase().startsWith("hub"))
                    b.hub.push(k);
                else (b[map[k]?.cat] ?? b.misc).push(k);
            }
            buckets = b;
        } catch (e) {
            error = String(e);
        }
    }

    const nameOf = (k) => map[k]?.name ?? k;

    const filtered = $derived.by(() => {
        const all = buckets[activeCat] ?? [];
        const q = query.trim().toLowerCase();
        const ks = q
            ? all.filter(
                  (k) =>
                      nameOf(k).toLowerCase().includes(q) ||
                      k.toLowerCase().includes(q),
              )
            : all;
        return { rows: ks.slice(0, CAP), total: ks.length };
    });

    async function save() {
        saving = true;
        saved = false;
        error = "";
        try {
            const stats = {};
            for (const k of Object.keys(raw.stats ?? {}))
                stats[k] = String(vals[k] ?? 0);
            const out = { ...raw, stats };
            await writeTextFile(PATH, JSON.stringify(out, null, 2));
            raw = out;
            saved = true;
        } catch (e) {
            error = String(e);
        } finally {
            saving = false;
        }
    }

    load();
</script>

<svelte:head>
    <title>Save File</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-4 p-6">
    <div>
        <h1 class="text-2xl font-semibold tracking-tight">Save File</h1>
    </div>

    {#if error}
        <div
            class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
        >
            {error}
        </div>
    {/if}

    {#if !raw && !error}
        <p class="text-muted-foreground text-sm">Loading MPProfile.json…</p>
    {:else if raw}
        <div class="flex flex-wrap gap-2">
            {#each CATS as c (c.id)}
                <Button
                    variant={activeCat === c.id ? "default" : "outline"}
                    size="sm"
                    onclick={() => {
                        activeCat = c.id;
                        query = "";
                    }}
                >
                    {c.label}
                    <span class="ml-1 opacity-70"
                        >{buckets[c.id]?.length ?? 0}</span
                    >
                </Button>
            {/each}
        </div>

        <div
            class="bg-background sticky top-0 z-10 flex items-center gap-3 py-2"
        >
            <Input
                placeholder="Search {activeCat}…"
                bind:value={query}
                class="max-w-sm"
            />
            <Button onclick={save} disabled={saving}
                >{saving ? "Saving…" : "Save"}</Button
            >
            {#if saved}<span class="text-sm text-green-600">Saved</span>{/if}
        </div>

        <Card.Root>
            <Card.Content class="grid gap-2 pt-6">
                {#each filtered.rows as k (k)}
                    <div
                        class="flex items-center justify-between gap-4 border-b pb-2 last:border-0 last:pb-0"
                    >
                        <div class="min-w-0 flex-1">
                            <Label for="f-{k}" class="block truncate text-sm"
                                >{nameOf(k)}</Label
                            >
                            {#if map[k]?.name}
                                <span
                                    class="text-muted-foreground font-mono text-[10px]"
                                    >{k}</span
                                >
                            {/if}
                        </div>
                        <Input
                            id="f-{k}"
                            type="number"
                            step={isFloat[k] ? "any" : "1"}
                            class="w-40 shrink-0"
                            bind:value={vals[k]}
                        />
                    </div>
                {/each}
                {#if filtered.rows.length === 0}
                    <p class="text-muted-foreground py-6 text-center text-sm">
                        No fields match.
                    </p>
                {/if}
            </Card.Content>
        </Card.Root>

        {#if filtered.total > CAP}
            <p class="text-muted-foreground text-center text-xs">
                Showing first {CAP} of {filtered.total} — refine your search to see
                more.
            </p>
        {/if}
    {/if}
</div>
