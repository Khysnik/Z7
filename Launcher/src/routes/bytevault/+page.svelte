<script>
    import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
    import * as Card from "$lib/components/ui/card/index.js";
    import { Button } from "$lib/components/ui/button/index.js";
    import { Input } from "$lib/components/ui/input/index.js";
    import { Label } from "$lib/components/ui/label/index.js";
    import { Switch } from "$lib/components/ui/switch/index.js";
    import { dataDir } from "$lib/paths.js";

    let PATH = "";

    const SKIP_SECTIONS = ["Options", "Unlocks"];
    const HIDE = /^(Plants_Stored|Zombies_Stored|StoredTeamId$|Team\d)/;
    const OPAQUE = /^\(?\d+\)?$/;

    let config = $state(null);
    let sections = $state([]);
    let error = $state("");
    let saving = $state(false);
    let saved = $state(false);

    const typeOf = (v) => typeof v === "boolean" ? "bool" : typeof v === "number" ? "int" : "str";

    async function load() {
        try {
            PATH = `${await dataDir()}\\bytevault.json`;
            const cfg = JSON.parse(await readTextFile(PATH));
            const data = cfg.data ?? {};
            const secs = [];
            for (const [name, obj] of Object.entries(data)) {
                if (
                    SKIP_SECTIONS.includes(name) ||
                    typeof obj !== "object" ||
                    obj === null
                )
                    continue;
                const fields = Object.entries(obj)
                    .filter(([k]) => !HIDE.test(k) && !OPAQUE.test(k))
                    .map(([k, v]) => ({ key: k, type: typeOf(v) }));
                if (fields.length) secs.push({ name, fields });
            }
            sections = secs;
            config = cfg;
        } catch (e) {
            error = String(e);
        }
    }

    async function save() {
        saving = true;
        saved = false;
        error = "";
        try {
            await writeTextFile(PATH, JSON.stringify(config, null, 2));
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
    <title>Bytevault</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
    <div>
        <h1 class="text-2xl font-semibold tracking-tight">Bytevault</h1>
    </div>

    {#if error}
        <div
            class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
        >
            {error}
        </div>
    {/if}

    {#if !config && !error}
        <p class="text-muted-foreground text-sm">Loading bytevault.json…</p>
    {:else if config}
        {#each sections as sec (sec.name)}
            <Card.Root>
                <Card.Header>
                    <Card.Title>{sec.name}</Card.Title>
                </Card.Header>
                <Card.Content class="grid gap-3">
                    {#each sec.fields as f (f.key)}
                        <div
                            class="flex items-center justify-between gap-4 border-b pb-3 last:border-0 last:pb-0"
                        >
                            <Label
                                for="{sec.name}-{f.key}"
                                class="font-mono text-xs break-all"
                                >{f.key}</Label
                            >
                            {#if f.type === "bool"}
                                <Switch
                                    id="{sec.name}-{f.key}"
                                    bind:checked={config.data[sec.name][f.key]}
                                />
                            {:else if f.type === "int"}
                                <Input
                                    id="{sec.name}-{f.key}"
                                    type="number"
                                    class="w-52 shrink-0"
                                    bind:value={config.data[sec.name][f.key]}
                                />
                            {:else}
                                <Input
                                    id="{sec.name}-{f.key}"
                                    class="w-52 shrink-0"
                                    bind:value={config.data[sec.name][f.key]}
                                />
                            {/if}
                        </div>
                    {/each}
                </Card.Content>
            </Card.Root>
        {/each}

        <div class="flex items-center gap-3">
            <Button onclick={save} disabled={saving}>
                {saving ? "Saving…" : "Save"}
            </Button>
            {#if saved}
                <span class="text-sm text-green-600"
                    >Saved to bytevault.json</span
                >
            {/if}
        </div>
    {/if}
</div>
