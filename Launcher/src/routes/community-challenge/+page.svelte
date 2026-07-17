<script>
  import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
  import { baseDir, dataDir } from "$lib/paths.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import { Textarea } from "$lib/components/ui/textarea/index.js";
  import { Label } from "$lib/components/ui/label/index.js";
  import { Switch } from "$lib/components/ui/switch/index.js";
  import ItemCombobox from "$lib/components/item-combobox.svelte";
  import MultiCombobox from "$lib/components/multi-combobox.svelte";
  import { communityChallengeEnabled } from "$lib/state.js";

  const REL = "\\data\\live\\communitychallenge.json";

  const NUM_FIELDS = [
    "userThreshold",
    "refreshRateSeconds",
    "proximityCooldownSeconds",
    "rotationBaseUnix",
    "rotationPeriodSecs",
  ];
  const TIER_NUM = ["bronzeThreshold", "silverThreshold", "goldThreshold"];

  const TYPE_OPTIONS = [
    { id: "elements", name: "Elements" },
    { id: "characters", name: "Characters" },
  ];
  const KEY_PATTERNS = {
    elements: /^c_Any(.+?)Character___kscx_g$/,
    characters: /^c_([A-Za-z0-9]+)__ks_g$/,
  };

  const CHAR_NAMES = {
    ps: "Peashooter",
    ch: "Chomper",
    ca: "Cactus",
    sf: "Sunflower",
    ci: "Citron",
    ro: "Rose",
    so: "Foot Soldier",
    imp: "Imp",
    en: "Engineer",
    sc: "Scientist",
    sup: "Super Brainz",
    pir: "Captain Deadbeard",
    as: "All-Star",
    hg: "Hover Goat-3000",
    pdno: "Dino Mech",
    zcat: "Cat Mech",
    p: "All Plants",
    z: "All Zombies",
  };

  let cfgPath = "";
  let config = $state(null);
  let trackType = $state("elements");
  let keys = $state([]);
  // Available keys per type, derived from the local profile's stat names.
  let keyOptionsByType = $state({ elements: [], characters: [] });
  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  const keyOptions = $derived(
    (keyOptionsByType[trackType] ?? []).map((id) => ({
      id,
      name: trackType === "characters" ? (CHAR_NAMES[id] ?? id) : id,
    })),
  );

  async function loadKeyOptions() {
    try {
      const prof = JSON.parse(
        await readTextFile(`${await dataDir()}\\MPProfile.json`),
      );
      const found = { elements: new Set(), characters: new Set() };
      for (const k of Object.keys(prof.stats ?? {})) {
        for (const [type, re] of Object.entries(KEY_PATTERNS)) {
          const m = k.match(re);
          if (m) {
            found[type].add(m[1]);
            break;
          }
        }
      }
      keyOptionsByType = {
        elements: [...found.elements].sort(),
        characters: [...found.characters].sort(),
      };
    } catch {
      // Profile unavailable — dropdowns just stay empty; save still works.
    }
  }

  async function load() {
    try {
      cfgPath = `${await baseDir()}${REL}`;
      const c = JSON.parse(await readTextFile(cfgPath));
      trackType = c.tracks?.type ?? "elements";
      keys = [...(c.tracks?.keys ?? [])];
      config = c;
      await loadKeyOptions();
    } catch (e) {
      error = String(e);
    }
  }

  // Switching type drops any selected keys that don't belong to the new type.
  function onTypeChange() {
    const valid = new Set(keyOptionsByType[trackType] ?? []);
    keys = keys.filter((k) => valid.has(k));
  }

  async function save() {
    saving = true;
    saved = false;
    error = "";
    try {
      const out = { ...config };
      for (const k of [...NUM_FIELDS, ...TIER_NUM]) out[k] = Number(out[k]) || 0;
      out.tracks = { type: trackType, keys: [...keys] };
      await writeTextFile(cfgPath, JSON.stringify(out, null, 1));
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
  <title>Community Challenge</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Community Challenge</h1>
  </div>

  <Card.Root>
    <Card.Content class="flex items-center justify-between gap-4 py-4">
      <Label for="ccEnabled" class="text-base">Enabled</Label>
      <Switch id="ccEnabled" bind:checked={$communityChallengeEnabled} />
    </Card.Content>
  </Card.Root>

  {#if error}
    <div class="border-destructive/50 text-destructive rounded-md border p-3 text-sm">
      {error}
    </div>
  {/if}

  {#if !config && !error}
    <p class="text-muted-foreground text-sm">Loading communitychallenge.json…</p>
  {:else if config}
    <Card.Root>
      <Card.Header><Card.Title>Challenge</Card.Title></Card.Header>
      <Card.Content class="grid gap-4">
        <div class="grid gap-2">
          <Label for="achievementId">achievementId</Label>
          <Input id="achievementId" bind:value={config.achievementId} />
        </div>
        <div class="grid gap-2">
          <Label for="name">name</Label>
          <Input id="name" bind:value={config.name} />
        </div>
        <div class="grid gap-2">
          <Label for="desc">desc</Label>
          <Textarea id="desc" rows={4} bind:value={config.desc} />
        </div>
        <div class="grid gap-2">
          <Label for="image">image (url)</Label>
          <Input id="image" bind:value={config.image} />
        </div>
        <div class="grid gap-4 sm:grid-cols-2">
          <div class="grid gap-2">
            <Label for="personalHeader">personalHeader</Label>
            <Input id="personalHeader" bind:value={config.personalHeader} />
          </div>
          <div class="grid gap-2">
            <Label for="rewardHeader">rewardHeader</Label>
            <Input id="rewardHeader" bind:value={config.rewardHeader} />
          </div>
        </div>
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Community goals</Card.Title>
      </Card.Header>
      <Card.Content class="grid gap-4">
        <div class="grid gap-2">
          <Label for="bronzeThreshold">bronze — threshold / reward</Label>
          <div class="grid gap-2 sm:grid-cols-2">
            <Input id="bronzeThreshold" type="number" bind:value={config.bronzeThreshold} />
            <Input bind:value={config.bronzeReward} placeholder="reward" />
          </div>
        </div>
        <div class="grid gap-2">
          <Label for="silverThreshold">silver — threshold / reward</Label>
          <div class="grid gap-2 sm:grid-cols-2">
            <Input id="silverThreshold" type="number" bind:value={config.silverThreshold} />
            <Input bind:value={config.silverReward} placeholder="reward" />
          </div>
        </div>
        <div class="grid gap-2">
          <Label for="goldThreshold">gold — threshold / reward</Label>
          <div class="grid gap-2 sm:grid-cols-2">
            <Input id="goldThreshold" type="number" bind:value={config.goldThreshold} />
            <Input bind:value={config.goldReward} placeholder="reward" />
          </div>
        </div>
        <div class="grid gap-2">
          <Label for="userThreshold">userThreshold (per-player goal)</Label>
          <Input id="userThreshold" type="number" bind:value={config.userThreshold} />
        </div>
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Tracked stats</Card.Title>
      </Card.Header>
      <Card.Content class="grid gap-4">
        <div class="grid gap-2">
          <Label>type</Label>
          <ItemCombobox
            items={TYPE_OPTIONS}
            bind:value={trackType}
            onselect={onTypeChange}
          />
        </div>
        <div class="grid gap-2">
          <Label>keys</Label>
          <MultiCombobox
            items={keyOptions}
            bind:values={keys}
            placeholder="Select keys…"
          />
        </div>
      </Card.Content>
    </Card.Root>

    <Card.Root>
      <Card.Header>
        <Card.Title>Misc</Card.Title>
      </Card.Header>
      <Card.Content class="grid gap-4 sm:grid-cols-2">
        {#each NUM_FIELDS as key (key)}
          <div class="grid gap-2">
            <Label for={key}>{key}</Label>
            <Input id={key} type="number" bind:value={config[key]} />
          </div>
        {/each}
      </Card.Content>
    </Card.Root>

    <div class="flex items-center gap-3">
      <Button onclick={save} disabled={saving}>{saving ? "Saving…" : "Save"}</Button>
      {#if saved}
        <span class="text-sm text-green-600">Saved to communitychallenge.json</span>
      {/if}
    </div>
  {/if}
</div>
