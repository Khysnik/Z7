<script>
  import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
  import { readSection } from "$lib/data.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import { Label } from "$lib/components/ui/label/index.js";
  import ItemCombobox from "$lib/components/item-combobox.svelte";
  import ThumbCombobox from "$lib/components/thumb-combobox.svelte";
  import XIcon from "@lucide/svelte/icons/x";
  import { dataDir, launcherDir } from "$lib/paths.js";

  let CONFIG_PATH = "";
  let ITEMMAP_PATH = "";

  const GH = "https://raw.githubusercontent.com/Khysnik/Z7/refs/heads/main/assets/images";
  const unlockImg = (id) => `${GH}/${id}.png`;
  const consumImg = (ckey) => `${GH}/consumables/${ckey}.png`;
  const currencyImg = (ckey) => `${GH}/currency/${ckey}.png`;
  const packImg = (imgn) => `${GH}/packs/${imgn.split("/").pop()}.png`;

  const CURRENCIES = ["Coinz", "RainbowStar", "Star"];

  let raw = $state(null);
  let currencies = $state([]);
  let consumables = $state([]);
  let unlocks = $state([]);

  let itemMap = $state({});
  let packsById = $state({});
  let addItems = $state([]);
  let consumCatalog = $state([]);
  let consumNames = $state({});

  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  function resolveUnlock(id) {
    if (itemMap[id]) return { label: itemMap[id], img: unlockImg(id) };
    const p = packsById[id];
    if (p) return { label: p.TITL || id, img: p.IMGN ? packImg(p.IMGN) : "" };
    return { label: id, img: "" };
  }

  async function load() {
    try {
      const data = await dataDir();
      const launcher = await launcherDir();
      CONFIG_PATH = `${data}\\inventory.json`;
      ITEMMAP_PATH = `${launcher}\\item_map.json`;
      const inv = JSON.parse(await readTextFile(CONFIG_PATH));
      itemMap = JSON.parse(await readTextFile(ITEMMAP_PATH));
      const packsFile = (await readSection("packs")) ?? {};
      packsById = Object.fromEntries(
        (packsFile.packs ?? []).map((p) => [p.PKEY, p]),
      );

      const items = inv.items ?? [];
      currencies = CURRENCIES.map((ckey) => ({
        ckey,
        qant: items.find((i) => i.ckey === ckey)?.qant ?? 0,
      }));
      consumables = items
        .filter((i) => !CURRENCIES.includes(i.ckey))
        .map((i) => ({ ckey: i.ckey, qant: i.qant }));
      unlocks = [...(inv.unlocks ?? [])];

      addItems = [
        ...Object.entries(itemMap).map(([id, name]) => ({ id, name })),
        ...(packsFile.packs ?? []).map((p) => ({
          id: p.PKEY,
          name: p.TITL || p.PKEY,
        })),
      ];

      consumNames = JSON.parse(
        await readTextFile(`${launcher}\\consumables.json`),
      );
      const keys = new Set(Object.keys(consumNames));
      for (const c of consumables) keys.add(c.ckey);
      consumCatalog = [...keys]
        .sort()
        .map((k) => ({
          value: k,
          label: consumNames[k] || k,
          img: consumImg(k),
        }));

      raw = inv;
    } catch (e) {
      error = String(e);
    }
  }

  function addConsumable(ckey) {
    if (ckey && !consumables.some((c) => c.ckey === ckey))
      consumables.push({ ckey, qant: 1 });
  }
  function removeConsumable(i) {
    consumables.splice(i, 1);
  }
  function addUnlock(id) {
    if (id && !unlocks.includes(id)) unlocks.unshift(id);
  }
  function removeUnlock(i) {
    unlocks.splice(i, 1);
  }

  async function save() {
    saving = true;
    saved = false;
    error = "";
    try {
      const items = [
        ...currencies
          .filter((c) => Number(c.qant) > 0)
          .map((c) => ({ ckey: c.ckey, qant: Number(c.qant) || 0 })),
        ...consumables.map((c) => ({
          ckey: c.ckey,
          qant: Number(c.qant) || 0,
        })),
      ];
      const out = { ...raw, items, unlocks: [...new Set(unlocks)] };
      await writeTextFile(CONFIG_PATH, JSON.stringify(out, null, 2));
      raw = out;
      saved = true;
    } catch (e) {
      error = String(e);
    } finally {
      saving = false;
    }
  }

  let addUnlockId = $state("");

  load();
</script>

<svelte:head>
  <title>Inventory</title>
</svelte:head>

<div class="mx-auto flex max-w-4xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Inventory</h1>
  </div>

  {#if error}
    <div
      class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
    >
      {error}
    </div>
  {/if}

  {#if !raw && !error}
    <p class="text-muted-foreground text-sm">Loading inventory.json…</p>
  {:else if raw}
    <Card.Root>
      <Card.Header><Card.Title>Currencies</Card.Title></Card.Header>
      <Card.Content class="grid gap-4 sm:grid-cols-3">
        {#each currencies as cur, i (cur.ckey)}
          <div class="grid gap-2">
            <Label for="cur-{cur.ckey}" class="flex items-center gap-2">
              <img
                src={currencyImg(cur.ckey)}
                alt=""
                class="size-5 object-contain"
              />
              {cur.ckey}
            </Label>
            <Input
              id="cur-{cur.ckey}"
              type="number"
              min="0"
              bind:value={currencies[i].qant}
            />
          </div>
        {/each}
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Consumables</Card.Title>
        <Card.Description>{consumables.length} items</Card.Description>
      </Card.Header>
      <Card.Content class="grid gap-3">
        <div class="grid gap-2 sm:max-w-sm">
          <Label>Add consumable</Label>
          <ThumbCombobox
            items={consumCatalog}
            placeholder="Select to add…"
            onselect={addConsumable}
          />
        </div>
        {#each consumables as c, i (c.ckey)}
          <div
            class="flex items-center gap-3 border-b pb-3 last:border-0 last:pb-0"
          >
            <img
              src={consumImg(c.ckey)}
              alt=""
              class="bg-muted size-10 shrink-0 rounded object-contain"
            />
            <span class="flex-1 truncate text-sm"
              >{consumNames[c.ckey] || c.ckey}</span
            >
            <span class="text-muted-foreground font-mono text-[10px]"
              >{c.ckey}</span
            >
            <Input
              type="number"
              min="0"
              class="w-28"
              bind:value={consumables[i].qant}
            />
            <Button
              variant="ghost"
              size="icon"
              onclick={() => removeConsumable(i)}
            >
              <XIcon class="size-4" />
            </Button>
          </div>
        {/each}
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Unlocks</Card.Title>
        <Card.Description>{unlocks.length} unlocked items</Card.Description>
      </Card.Header>
      <Card.Content class="grid gap-3">
        <div class="grid gap-2 sm:max-w-sm">
          <Label>Add unlock</Label>
          <ItemCombobox
            items={addItems}
            bind:value={addUnlockId}
            onselect={addUnlock}
          />
        </div>
        <div class="grid max-h-[28rem] gap-2 overflow-y-auto pr-1">
          {#each unlocks as id, i (id)}
            {@const u = resolveUnlock(id)}
            <div class="flex items-center gap-3">
              <img
                src={u.img}
                alt=""
                class="bg-muted size-9 shrink-0 rounded object-contain"
              />
              <span class="flex-1 truncate text-sm">{u.label}</span>
              <span class="text-muted-foreground font-mono text-[10px]"
                >{id}</span
              >
              <Button
                variant="ghost"
                size="icon"
                onclick={() => removeUnlock(i)}
              >
                <XIcon class="size-4" />
              </Button>
            </div>
          {/each}
        </div>
      </Card.Content>
    </Card.Root>

    <div class="flex items-center gap-3">
      <Button onclick={save} disabled={saving}
        >{saving ? "Saving…" : "Save"}</Button
      >
      {#if saved}<span class="text-sm text-green-600"
          >Saved to inventory.json</span
        >{/if}
    </div>
  {/if}
</div>
