<script>
  import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
  import { readSection } from "$lib/data.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import MinusIcon from "@lucide/svelte/icons/minus";
  import PlusIcon from "@lucide/svelte/icons/plus";
  import PackageIcon from "@lucide/svelte/icons/package";
  import { dataDir } from "$lib/paths.js";

  const GH = "https://raw.githubusercontent.com/Khysnik/Z7/refs/heads/main/assets/images";
  const packImg = (imgn) =>
    imgn ? `${GH}/packs/${imgn.split("/").pop()}.png` : "";

  const REGULAR = "CardPackType_Regular";
  const INF = "storepkinf"; // the store "info" tile, not a real pack

  let INV_PATH = "";
  let raw = $state(null);
  // Rows shown in the editor: the created store packs + community chest packs.
  let rows = $state([]); // [{ pkey, title, img }]
  let counts = $state({}); // pkey -> owned count

  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  const total = $derived(
    Object.values(counts).reduce((n, c) => n + (Number(c) || 0), 0),
  );

  async function load() {
    try {
      INV_PATH = `${await dataDir()}\\inventory.json`;
      const inv = JSON.parse(await readTextFile(INV_PATH));
      const community = (await readSection("community")) ?? {};
      const packsFile = (await readSection("packs")) ?? {};
      const fixedPacks = (await readSection("fixed_packs")) ?? {};
      const packsById = Object.fromEntries(
        (packsFile.packs ?? []).map((p) => [p.PKEY, p]),
      );
      const packRow = (pkey, title) => ({
        pkey,
        title: title || packsById[pkey]?.TITL || pkey,
        img: packImg(packsById[pkey]?.IMGN),
      });

      // Only actually-created packs are addable: the store packs (Minions and any
      // user-added CardPackType_Regular pack) and the community chest packs.
      const store = (packsFile.packs ?? [])
        .filter((p) => (p.TYPE ?? []).includes(REGULAR) && p.PKEY !== INF)
        .map((p) => packRow(p.PKEY, p.TITL));
      const portal = (community.portalChests ?? []).map((c) =>
        packRow(c.pkey, c.title),
      );
      // Deluxe / fixed-content packs (emoji accessory sets, coins starter).
      const deluxe = Object.keys(fixedPacks).map((pkey) => packRow(pkey));

      // Collapse giftPacks (a flat array with duplicates) to a per-pkey count.
      const gp = inv.giftPacks ?? [];
      const cnt = {};
      for (const k of gp) cnt[k] = (cnt[k] || 0) + 1;
      counts = cnt;

      // Any owned pack not in the created set still gets a row so it can be removed.
      const known = new Set([...store, ...portal, ...deluxe].map((r) => r.pkey));
      const extras = [...new Set(gp)]
        .filter((k) => !known.has(k))
        .map((k) => packRow(k));

      // Dedup by pkey — a deluxe pack can be both a Regular store pack and in fixed_packs.
      const seen = new Set();
      rows = [...store, ...portal, ...deluxe, ...extras].filter((r) =>
        seen.has(r.pkey) ? false : seen.add(r.pkey),
      );
      raw = inv;
    } catch (e) {
      error = String(e);
    }
  }

  function inc(pkey) {
    counts[pkey] = (Number(counts[pkey]) || 0) + 1;
  }
  function dec(pkey) {
    counts[pkey] = Math.max(0, (Number(counts[pkey]) || 0) - 1);
  }

  async function save() {
    saving = true;
    saved = false;
    error = "";
    try {
      // Re-read so a concurrent Inventory edit (items/unlocks) isn't clobbered.
      const inv = JSON.parse(await readTextFile(INV_PATH));
      const giftPacks = [];
      for (const { pkey } of rows) {
        const n = Number(counts[pkey]) || 0;
        for (let i = 0; i < n; i++) giftPacks.push(pkey);
      }
      inv.giftPacks = giftPacks;
      await writeTextFile(INV_PATH, JSON.stringify(inv, null, 2));
      raw = inv;
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
  <title>Gift Packs</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Gift Packs</h1>
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
      <Card.Header>
        <Card.Title>Owned Gift Packs</Card.Title>
        <Card.Description
          >{total} pack{total === 1 ? "" : "s"} total</Card.Description
        >
      </Card.Header>
      <Card.Content class="grid gap-3">
        {#each rows as row (row.pkey)}
          <div
            class="flex items-center gap-3 border-b pb-3 last:border-0 last:pb-0"
          >
            {#if row.img}
              <img
                src={row.img}
                alt=""
                class="bg-muted size-10 shrink-0 rounded object-contain"
              />
            {:else}
              <div
                class="bg-muted text-muted-foreground grid size-10 shrink-0 place-items-center rounded"
              >
                <PackageIcon class="size-5" />
              </div>
            {/if}
            <span class="flex-1 truncate text-sm">{row.title}</span>
            <span class="text-muted-foreground font-mono text-[10px]"
              >{row.pkey}</span
            >
            <div class="flex items-center gap-1">
              <Button
                variant="outline"
                size="icon"
                class="size-8"
                onclick={() => dec(row.pkey)}
                disabled={!counts[row.pkey]}
              >
                <MinusIcon class="size-4" />
              </Button>
              <Input
                type="number"
                min="0"
                class="w-16 text-center"
                bind:value={counts[row.pkey]}
              />
              <Button
                variant="outline"
                size="icon"
                class="size-8"
                onclick={() => inc(row.pkey)}
              >
                <PlusIcon class="size-4" />
              </Button>
            </div>
          </div>
        {/each}
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
