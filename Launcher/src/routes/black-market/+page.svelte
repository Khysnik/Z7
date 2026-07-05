<script>
  import { readTextFile } from "@tauri-apps/plugin-fs";
  import { readSection, writeSection } from "$lib/data.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import { Textarea } from "$lib/components/ui/textarea/index.js";
  import { Label } from "$lib/components/ui/label/index.js";
  import ItemCombobox from "$lib/components/item-combobox.svelte";
  import { launcherDir } from "$lib/paths.js";

  let ITEMMAP_PATH = "";
  const cdn = (id) => `https://raw.githubusercontent.com/Khysnik/Z7/refs/heads/main/assets/images/${id}.png`;
  const SLOT_COUNT = 5;

  let config = $state(null);
  let items = $state([]);
  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  let haveText = $state("");
  let noText = $state("");
  let decideText = $state("");

  async function load() {
    try {
      ITEMMAP_PATH = `${await launcherDir()}\\item_map.json`;
      const cfg = (await readSection("blackmarket")) ?? {};
      const map = JSON.parse(await readTextFile(ITEMMAP_PATH));
      items = Object.entries(map).map(([id, name]) => ({ id, name }));

      const slots = cfg.slots ?? [];
      while (slots.length < SLOT_COUNT) {
        slots.push({
          slid: `z7bm${slots.length + 1}`,
          itid: "",
          name: "",
          desc: "",
          price: 0,
        });
      }
      cfg.slots = slots.slice(0, SLOT_COUNT);

      haveText = (cfg.haveItemsDialog ?? []).join("\n");
      noText = (cfg.noItemsDialog ?? []).join("\n");
      decideText = (cfg.stillDecidingDialog ?? []).join("\n");
      config = cfg;
    } catch (e) {
      error = String(e);
    }
  }

  const toLines = (s) =>
    s
      .split("\n")
      .map((l) => l.trim())
      .filter(Boolean);

  const itemName = (id) => items.find((i) => i.id === id)?.name ?? "";

  async function save() {
    saving = true;
    saved = false;
    error = "";
    try {
      const out = {
        ...config,
        haveItemsDialog: toLines(haveText),
        noItemsDialog: toLines(noText),
        stillDecidingDialog: toLines(decideText),
        slots: config.slots.map((s) => ({ ...s, price: Number(s.price) || 0 })),
      };
      await writeSection("blackmarket", out);
      config = out;
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
  <title>Black Market</title>
</svelte:head>

<div class="mx-auto flex max-w-5xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Black Market</h1>
  </div>

  {#if error}
    <div
      class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
    >
      {error}
    </div>
  {/if}

  {#if !config && !error}
    <p class="text-muted-foreground text-sm">Loading config…</p>
  {:else if config}
    <Card.Root>
      <Card.Header>
        <Card.Title>Dialog</Card.Title>
      </Card.Header>
      <Card.Content class="grid gap-4 sm:grid-cols-3">
        <div class="grid gap-2">
          <Label for="have">Have items</Label>
          <Textarea id="have" rows={5} bind:value={haveText} />
        </div>
        <div class="grid gap-2">
          <Label for="no">No items</Label>
          <Textarea id="no" rows={5} bind:value={noText} />
        </div>
        <div class="grid gap-2">
          <Label for="decide">Still deciding</Label>
          <Textarea id="decide" rows={5} bind:value={decideText} />
        </div>
      </Card.Content>
    </Card.Root>

    <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
      {#each config.slots as slot, i (slot.slid)}
        <Card.Root>
          <Card.Header>
            <Card.Title class="text-base">Slot {i + 1}</Card.Title>
            <Card.Description class="font-mono text-xs"
              >{slot.slid}</Card.Description
            >
          </Card.Header>
          <Card.Content class="grid gap-3">
            <div
              class="bg-muted flex aspect-square items-center justify-center overflow-hidden rounded-md"
            >
              {#if slot.itid}
                <img
                  src={cdn(slot.itid)}
                  alt={itemName(slot.itid)}
                  class="size-full object-contain"
                  loading="lazy"
                />
              {:else}
                <span class="text-muted-foreground text-xs"
                  >No item selected</span
                >
              {/if}
            </div>

            <div class="grid gap-2">
              <Label>Item</Label>
              <ItemCombobox {items} bind:value={config.slots[i].itid} />
            </div>

            <div class="grid gap-2">
              <Label for="price-{i}">Price</Label>
              <Input
                id="price-{i}"
                type="number"
                min="0"
                bind:value={config.slots[i].price}
              />
            </div>
          </Card.Content>
        </Card.Root>
      {/each}
    </div>

    <div class="flex items-center gap-3">
      <Button onclick={save} disabled={saving}>
        {saving ? "Saving…" : "Save"}
      </Button>
      {#if saved}
        <span class="text-sm text-green-600">Saved to data.json</span>
      {/if}
    </div>
  {/if}
</div>
