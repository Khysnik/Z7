<script>
  import { readSection, writeSection } from "$lib/data.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Switch } from "$lib/components/ui/switch/index.js";
  import { Label } from "$lib/components/ui/label/index.js";

  const GROUPS = [
    {
      title: "Features",
      items: [
        ["MenchiesChallengeLicense", "menchiesenabled"],
        ["BlackMarketGnomeLicense", "_aLcHB0sy_kyJdb"],
      ],
    },
    {
      title: "Time Trials",
      items: [
        ["PlantTimeTrialsLicense", "ptimechallenge"],
        ["ZombieTimeTrialsLicense", "ztimechallenge"],
      ],
    },
    {
      title: "Gnome Targets",
      items: [
        ["PlantGnomeTargetsMinigameLicense", "_aLcHB0sy_kyJdb"],
        ["ZombieGnomeTargetsMinigameLicense", "_aLcHB0sy_kyJdb"],
        ["GnomeTargetsLeaderboardLicense", "_aLcHB0sy_kyJdb"],
      ],
    },
    {
      title: "Seasonal Events",
      items: [
        ["Halloween2016License", "doom2016"],
        ["Halloween2017License", "doom2017"],
        ["Festivus2016License", "festive2016"],
        ["Festivus2017License", "festive2017"],
        ["Springening2017License", "springen2017"],
        ["LuckOZombie2017License", "lucko2017"],
      ],
    },
    {
      title: "Festival Weeks (2019)",
      items: [
        ["FestivalWeek1", "2019festweek1"],
        ["FestivalWeek2", "2019festweek2"],
        ["FestivalWeek3", "2019festweek3"],
        ["FestivalWeek4", "2019festweek4"],
        ["FestivalWeek5", "2019festweek5"],
        ["FestivalWeek6", "2019festweek6"],
        ["FestivalWeek7", "2019festweek7"],
        ["FestivalWeek8", "2019festweek8"],
      ],
    },
    {
      title: "Compliance / Disable",
      items: [
        ["UnderageLicense", "underagedisable"],
        ["GdprStopProcessLicense", "gdprdisable"],
        ["MarketingOptOutLicense", "marketdisable"],
        ["UpsellDisable", "upselldisable"],
        ["LoyaltyDisable", "loyaltydisable"],
        ["AccessDisable", "accessdisable"],
      ],
    },
  ];

  const label = (name) =>
    name
      .replace(/License$/, "")
      .replace(/([a-z])([A-Z])/g, "$1 $2")
      .replace(/([A-Za-z])(\d)/g, "$1 $2")
      .trim();

  let raw = $state(null);
  let licenses = $state([]); // string[]
  let error = $state("");
  let saving = $state(false);
  let saved = $state(false);

  async function load() {
    try {
      raw = (await readSection("extra_licenses")) ?? {};
      licenses = [...(raw.licenses ?? [])];
    } catch (e) {
      error = String(e);
    }
  }

  const isOn = (code) => licenses.includes(code);

  function setCode(code, on) {
    const s = new Set(licenses);
    if (on) s.add(code);
    else s.delete(code);
    licenses = [...s];
  }

  async function save() {
    saving = true;
    saved = false;
    error = "";
    try {
      const out = { ...raw, licenses: [...new Set(licenses)] };
      await writeSection("extra_licenses", out);
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
  <title>Killswitches</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
  <div>
    <h1 class="text-2xl font-semibold tracking-tight">Killswitches</h1>
  </div>

  {#if error}
    <div
      class="border-destructive/50 text-destructive rounded-md border p-3 text-sm"
    >
      {error}
    </div>
  {/if}

  {#if !raw && !error}
    <p class="text-muted-foreground text-sm">Loading data.json…</p>
  {:else if raw}
    {#each GROUPS as group (group.title)}
      <Card.Root>
        <Card.Header><Card.Title>{group.title}</Card.Title></Card.Header>
        <Card.Content class="grid gap-3">
          {#each group.items as [name, code] (name)}
            <div
              class="flex items-center justify-between gap-4 border-b pb-3 last:border-0 last:pb-0"
            >
              <div class="min-w-0">
                <Label for={name}>{label(name)}</Label>
                <p class="text-muted-foreground font-mono text-[10px]">
                  {code}
                </p>
              </div>
              <Switch
                id={name}
                checked={isOn(code)}
                onCheckedChange={(v) => setCode(code, v)}
              />
            </div>
          {/each}
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
