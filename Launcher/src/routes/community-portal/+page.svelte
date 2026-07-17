<script>
  import { readDir } from "@tauri-apps/plugin-fs";
  import { readSection, writeSection } from "$lib/data.js";
  import { convertFileSrc } from "@tauri-apps/api/core";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import { Textarea } from "$lib/components/ui/textarea/index.js";
  import { Label } from "$lib/components/ui/label/index.js";
  import { Switch } from "$lib/components/ui/switch/index.js";
  import ThumbCombobox from "$lib/components/thumb-combobox.svelte";
  import { baseDir } from "$lib/paths.js";
  import { communityPortalEnabled } from "$lib/state.js";

  let CP_IMG_DIR = "";

  const IMG_BASE = "https://localhost:42220/PlantsVsZombies/GW2/image/cp/";

  const packImg = (imgn) => `https://raw.githubusercontent.com/Khysnik/Z7/refs/heads/main/assets/images/packs/${imgn.split("/").pop()}.png`;

  const NUM_FIELDS = [
    "startOffsetDays",
    "endOffsetDays",
    "grandPrizeOffsetDays",
    "firstChestScore",
    "secondChestScore",
    "thirdChestScore",
  ];

  let config = $state(null);
  let packs = $state([]);
  let images = $state([]);
  let imageName = $state("");
  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  async function load() {
    try {
      CP_IMG_DIR = `${await baseDir()}\\files\\GW2\\image\\cp`;
      const cfg = (await readSection("community")) ?? {};
      const packsFile = (await readSection("packs")) ?? {};

      packs = (packsFile.packs ?? [])
        .filter((p) => (p.TYPE ?? []).includes("CardPackType_Regular"))
        .map((p) => ({
          value: p.PKEY,
          label: p.TITL || p.PKEY,
          img: p.IMGN ? packImg(p.IMGN) : "",
        }));

      const entries = await readDir(CP_IMG_DIR);
      images = entries
        .filter((e) => e.isFile && /\.(png|jpe?g|webp)$/i.test(e.name))
        .map((e) => ({
          value: e.name,
          label: e.name,
          img: convertFileSrc(`${CP_IMG_DIR}\\${e.name}`),
        }))
        .sort((a, b) => a.label.localeCompare(b.label));

      imageName = (cfg.imageUrl ?? "").split("/").pop() ?? "";
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
      const out = {
        ...config,
        imageUrl: imageName ? IMG_BASE + imageName : "",
      };
      for (const k of NUM_FIELDS) out[k] = Number(out[k]) || 0;
      await writeSection("community", out);
      config = out;
      saved = true;
    } catch (e) {
      error = String(e);
    } finally {
      saving = false;
    }
  }

  const selectedImg = $derived(images.find((i) => i.value === imageName)?.img);

  load();
</script>

<svelte:head>
  <title>Community Portal</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Community Portal</h1>
  </div>

  <Card.Root>
    <Card.Content class="flex items-center justify-between gap-4 py-4">
      <Label for="cpEnabled" class="text-base">Enabled</Label>
      <Switch id="cpEnabled" bind:checked={$communityPortalEnabled} />
    </Card.Content>
  </Card.Root>

  {#if error}
    <div
      class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
    >
      {error}
    </div>
  {/if}

  {#if !config && !error}
    <p class="text-muted-foreground text-sm">Loading data.json…</p>
  {:else if config}
    <Card.Root>
      <Card.Header><Card.Title>Event</Card.Title></Card.Header>
      <Card.Content class="grid gap-4">
        <div class="flex items-center justify-between gap-4 border-b pb-3">
          <Label for="featured">featured</Label>
          <Switch id="featured" bind:checked={config.featured} />
        </div>
        <div class="grid gap-2">
          <Label for="eventId">eventId</Label>
          <Input id="eventId" bind:value={config.eventId} />
        </div>
        <div class="grid gap-2">
          <Label for="name">name</Label>
          <Input id="name" bind:value={config.name} />
        </div>
        <div class="grid gap-2">
          <Label for="description">description</Label>
          <Textarea id="description" rows={4} bind:value={config.description} />
        </div>

        <div class="grid gap-2">
          <Label>image</Label>
          <div class="flex items-start gap-3">
            <div class="bg-muted size-24 shrink-0 overflow-hidden rounded-md">
              {#if selectedImg}
                <img
                  src={selectedImg}
                  alt={imageName}
                  class="size-full object-cover"
                />
              {/if}
            </div>
            <div class="flex-1">
              <ThumbCombobox
                items={images}
                bind:value={imageName}
                placeholder="Select an image…"
              />
              <p class="text-muted-foreground mt-1 text-xs break-all">
                {imageName ? IMG_BASE + imageName : "No image selected"}
              </p>
            </div>
          </div>
        </div>
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header><Card.Title>Schedule &amp; scoring</Card.Title></Card.Header>
      <Card.Content class="grid gap-4 sm:grid-cols-2">
        {#each NUM_FIELDS as key (key)}
          <div class="grid gap-2">
            <Label for={key}>{key}</Label>
            <Input id={key} type="number" bind:value={config[key]} />
          </div>
        {/each}
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Rewards</Card.Title>
        <Card.Description>Regular card packs from the packs catalog.</Card.Description>
      </Card.Header>
      <Card.Content class="grid gap-4">
        <div class="grid gap-2">
          <Label>chestReward</Label>
          <ThumbCombobox
            items={packs}
            bind:value={config.chestReward}
            placeholder="Select a pack…"
          />
        </div>
        <div class="grid gap-2">
          <Label>eventReward</Label>
          <ThumbCombobox
            items={packs}
            bind:value={config.eventReward}
            placeholder="Select a pack…"
          />
        </div>
      </Card.Content>
    </Card.Root>

    <div class="flex items-center gap-3">
      <Button onclick={save} disabled={saving}
        >{saving ? "Saving…" : "Save"}</Button
      >
      {#if saved}
        <span class="text-sm text-green-600">Saved to data.json</span>
      {/if}
    </div>
  {/if}
</div>
