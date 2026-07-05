<script>
    import { readSection, writeSection } from "$lib/data.js";
    import * as Card from "$lib/components/ui/card/index.js";
    import { Button } from "$lib/components/ui/button/index.js";
    import { Input } from "$lib/components/ui/input/index.js";
    import { Label } from "$lib/components/ui/label/index.js";
    import { Switch } from "$lib/components/ui/switch/index.js";
    import ChevronUpIcon from "@lucide/svelte/icons/chevron-up";
    import ChevronDownIcon from "@lucide/svelte/icons/chevron-down";
    import Trash2Icon from "@lucide/svelte/icons/trash-2";
    import PlusIcon from "@lucide/svelte/icons/plus";
    const REGULAR = "CardPackType_Regular";
    const INF = "storepkinf"; // no loot table
    const SOURCES = ["item", "consumable", "sticker", "stickerSet"];
    const selectClass =
        "border-input bg-background h-9 rounded-md border px-2 text-sm shadow-xs";

    let rarities = $state([
        "Common",
        "Uncommon",
        "Rare",
        "SuperRare",
        "Legendary",
    ]);
    let tables = $state([]); // [{ id, name, cost, slots: [...] }]
    let loaded = $state(false);
    let error = $state("");
    let saving = $state(false);
    let saved = $state(false);

    const toEditor = (s = {}) => ({
        repeat: s.repeat ?? 1,
        source: s.source ?? "item",
        minRarity: s.minRarity ?? "",
        rarity: s.rarity ?? "",
        chance: s.chance ?? "",
        else: s.else ?? "",
        rarityIn: s.rarityIn ? [...s.rarityIn] : [],
        excludeLegendary: !!s.excludeLegendary,
    });

    function cleanSlot(s) {
        const o = {};
        if (Number(s.repeat) > 1) o.repeat = Number(s.repeat);
        o.source = s.source || "item";
        if (s.minRarity) o.minRarity = s.minRarity;
        if (s.rarity) o.rarity = s.rarity;
        if (s.chance !== "" && s.chance != null && Number(s.chance) > 0)
            o.chance = Number(s.chance);
        if (s.else) o.else = s.else;
        if (s.rarityIn?.length) o.rarityIn = [...s.rarityIn];
        if (s.excludeLegendary) o.excludeLegendary = true;
        return o;
    }

    async function load() {
        try {
            const packsFile = (await readSection("packs")) ?? {};
            const tablesFile = (await readSection("pack_tables")) ?? {};
            if (tablesFile.rarityOrder?.length)
                rarities = tablesFile.rarityOrder;
            const existing = tablesFile.packs ?? {};

            tables = (packsFile.packs ?? [])
                .filter(
                    (p) => (p.TYPE ?? []).includes(REGULAR) && p.PKEY !== INF,
                )
                .map((p) => ({
                    id: p.PKEY,
                    name: p.TITL || p.PKEY,
                    cost: p.PRIC ?? 0,
                    slots: (existing[p.PKEY]?.slots ?? []).map(toEditor),
                }));
            loaded = true;
        } catch (e) {
            error = String(e);
        }
    }

    const move = (slots, i, dir) => {
        const j = i + dir;
        if (j < 0 || j >= slots.length) return;
        [slots[i], slots[j]] = [slots[j], slots[i]];
    };
    const toggleRarityIn = (slot, r) => {
        const s = new Set(slot.rarityIn);
        s.has(r) ? s.delete(r) : s.add(r);
        slot.rarityIn = [...s];
    };

    async function save() {
        saving = true;
        saved = false;
        error = "";
        try {
            const packs = {};
            for (const t of tables)
                packs[t.id] = {
                    name: t.name,
                    cost: Number(t.cost) || 0,
                    slots: t.slots.map(cleanSlot),
                };
            const out = { rarityOrder: rarities, packs };
            await writeSection("pack_tables", out);
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
    <title>Pack Tables</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
    <div>
        <h1 class="text-2xl font-semibold tracking-tight">Pack Tables</h1>
    </div>

    {#if error}
        <div
            class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
        >
            {error}
        </div>
    {/if}

    {#if !loaded && !error}
        <p class="text-muted-foreground text-sm">Loading data.json…</p>
    {:else if loaded}
        {#each tables as table (table.id)}
            <Card.Root>
                <Card.Header>
                    <div class="flex items-baseline justify-between">
                        <Card.Title class="text-base">{table.name}</Card.Title>
                        <span class="text-muted-foreground font-mono text-xs"
                            >{table.id} · {table.cost}</span
                        >
                    </div>
                </Card.Header>
                <Card.Content class="grid gap-3">
                    {#each table.slots as slot, i (i)}
                        <div
                            class="bg-muted/40 grid gap-2 rounded-md border p-3"
                        >
                            <div class="flex items-center justify-between">
                                <span
                                    class="text-muted-foreground text-xs font-medium"
                                    >Slot {i + 1}</span
                                >
                                <div class="flex items-center gap-1">
                                    <Button
                                        variant="ghost"
                                        size="icon"
                                        disabled={i === 0}
                                        onclick={() => move(table.slots, i, -1)}
                                    >
                                        <ChevronUpIcon class="size-4" />
                                    </Button>
                                    <Button
                                        variant="ghost"
                                        size="icon"
                                        disabled={i === table.slots.length - 1}
                                        onclick={() => move(table.slots, i, 1)}
                                    >
                                        <ChevronDownIcon class="size-4" />
                                    </Button>
                                    <Button
                                        variant="ghost"
                                        size="icon"
                                        onclick={() => table.slots.splice(i, 1)}
                                    >
                                        <Trash2Icon class="size-4" />
                                    </Button>
                                </div>
                            </div>

                            <div class="grid grid-cols-2 gap-2 sm:grid-cols-4">
                                <div class="grid gap-1">
                                    <Label class="text-xs">Repeat</Label>
                                    <Input
                                        type="number"
                                        min="1"
                                        bind:value={slot.repeat}
                                    />
                                </div>
                                <div class="grid gap-1">
                                    <Label class="text-xs">Source</Label>
                                    <select
                                        class={selectClass}
                                        bind:value={slot.source}
                                    >
                                        {#each SOURCES as s}<option value={s}
                                                >{s}</option
                                            >{/each}
                                    </select>
                                </div>
                                <div class="grid gap-1">
                                    <Label class="text-xs">Min rarity</Label>
                                    <select
                                        class={selectClass}
                                        bind:value={slot.minRarity}
                                    >
                                        <option value="">—</option>
                                        {#each rarities as r}<option value={r}
                                                >{r}</option
                                            >{/each}
                                    </select>
                                </div>
                                <div class="grid gap-1">
                                    <Label class="text-xs">Exact rarity</Label>
                                    <select
                                        class={selectClass}
                                        bind:value={slot.rarity}
                                    >
                                        <option value="">—</option>
                                        {#each rarities as r}<option value={r}
                                                >{r}</option
                                            >{/each}
                                    </select>
                                </div>
                                <div class="grid gap-1">
                                    <Label class="text-xs">Chance</Label>
                                    <Input
                                        type="number"
                                        min="0"
                                        max="1"
                                        step="0.05"
                                        bind:value={slot.chance}
                                    />
                                </div>
                                <div class="grid gap-1">
                                    <Label class="text-xs">Else source</Label>
                                    <select
                                        class={selectClass}
                                        bind:value={slot.else}
                                    >
                                        <option value="">—</option>
                                        {#each SOURCES as s}<option value={s}
                                                >{s}</option
                                            >{/each}
                                    </select>
                                </div>
                                <div
                                    class="col-span-2 flex items-end gap-2 pb-1"
                                >
                                    <Switch
                                        id="{table.id}-{i}-excl"
                                        bind:checked={slot.excludeLegendary}
                                    />
                                    <Label
                                        for="{table.id}-{i}-excl"
                                        class="text-xs">Exclude Legendary</Label
                                    >
                                </div>
                            </div>

                            <div class="grid gap-1">
                                <Label class="text-xs"
                                    >Rarity set (rarityIn)</Label
                                >
                                <div class="flex flex-wrap gap-3">
                                    {#each rarities as r}
                                        <label
                                            class="flex items-center gap-1 text-xs"
                                        >
                                            <input
                                                type="checkbox"
                                                checked={slot.rarityIn.includes(
                                                    r,
                                                )}
                                                onchange={() =>
                                                    toggleRarityIn(slot, r)}
                                            />
                                            {r}
                                        </label>
                                    {/each}
                                </div>
                            </div>
                        </div>
                    {/each}

                    <Button
                        variant="outline"
                        size="sm"
                        class="justify-self-start"
                        onclick={() => table.slots.push(toEditor())}
                    >
                        <PlusIcon class="mr-1 size-4" /> Add slot
                    </Button>
                </Card.Content>
            </Card.Root>
        {/each}

        <div class="flex items-center gap-3">
            <Button onclick={save} disabled={saving}
                >{saving ? "Saving…" : "Save"}</Button
            >
            {#if saved}<span class="text-sm text-green-600"
                    >Saved to data.json</span
                >{/if}
        </div>
    {/if}
</div>
