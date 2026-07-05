<script>
   import { readTextFile } from "@tauri-apps/plugin-fs";
   import { readSection, writeSection } from "$lib/data.js";
   import * as Card from "$lib/components/ui/card/index.js";
   import { Button } from "$lib/components/ui/button/index.js";
   import { Input } from "$lib/components/ui/input/index.js";
   import { Textarea } from "$lib/components/ui/textarea/index.js";
   import { Label } from "$lib/components/ui/label/index.js";
   import ThumbCombobox from "$lib/components/thumb-combobox.svelte";
   import ChevronUpIcon from "@lucide/svelte/icons/chevron-up";
   import ChevronDownIcon from "@lucide/svelte/icons/chevron-down";
   import Trash2Icon from "@lucide/svelte/icons/trash-2";
   import PlusIcon from "@lucide/svelte/icons/plus";
   import { launcherDir } from "$lib/paths.js";

   let IMG_MANIFEST = "";
   const IMG_PREFIX = "UI/Assets/Packs/";
   const REGULAR = "CardPackType_Regular";
   const INF = "storepkinf"; // always pinned last
   const packImg = (imgn) => imgn ? `https://raw.githubusercontent.com/Khysnik/Z7/refs/heads/main/assets/images/packs/${imgn.split("/").pop()}.png` : "";

   let raw = $state(null);
   let other = $state([]);
   let packs = $state([]);
   let inf = $state(null);
   let imageOptions = $state([]);
   let tablesRaw = null;
   let allKeys = new Set();
   let error = $state("");
   let saving = $state(false);
   let saved = $state(false);

   async function load() {
      try {
         IMG_MANIFEST = `${await launcherDir()}\\pack_images.json`;
         raw = (await readSection("packs")) ?? {};
         const list = raw.packs ?? [];
         allKeys = new Set(list.map((p) => p.PKEY));
         other = list.filter((p) => !(p.TYPE ?? []).includes(REGULAR));
         const reg = list.filter((p) => (p.TYPE ?? []).includes(REGULAR));
         inf = reg.find((p) => p.PKEY === INF) ?? null;
         packs = reg.filter((p) => p.PKEY !== INF);

         const names = JSON.parse(await readTextFile(IMG_MANIFEST));
         imageOptions = names.map((n) => ({
            value: IMG_PREFIX + n,
            label: n,
            img: packImg(IMG_PREFIX + n),
         }));

         tablesRaw = (await readSection("pack_tables")) ?? {};
      } catch (e) {
         error = String(e);
      }
   }

   function move(i, dir) {
      const j = i + dir;
      if (j < 0 || j >= packs.length) return;
      [packs[i], packs[j]] = [packs[j], packs[i]];
   }

   function nextPkey() {
      const nums = packs
         .map((p) => p.PKEY)
         .filter((k) => /^dynpk\d+$/.test(k))
         .map((k) => +k.slice(5));
      let n = (nums.length ? Math.max(...nums) : 268) + 1;
      while (allKeys.has(`dynpk${n}`)) n++;
      return `dynpk${n}`;
   }

   function addPack() {
      const key = nextPkey();
      allKeys.add(key);
      packs.push({
         PKEY: key,
         CONS: "Coinz",
         PRIC: 2500,
         AUDL: 65,
         STID: 0,
         STRK: 0,
         TITL: "New Pack",
         DESC: "",
         ADDT: "",
         GKEY: "",
         IMGN: "UI/Assets/Packs/pack_minions",
         TYPE: [REGULAR],
      });
   }

   function removePack(i) {
      packs.splice(i, 1);
   }

   async function save() {
      saving = true;
      saved = false;
      error = "";
      try {
         const reg = [
            ...packs.map((p) => ({ ...p, PRIC: Number(p.PRIC) || 0 })),
            ...(inf ? [inf] : []),
         ];
         const out = { ...raw, packs: [...other, ...reg] };
         await writeSection("packs", out);
         raw = out;

         if (tablesRaw) {
            const prev = tablesRaw.packs ?? {};
            const tp = {};
            for (const p of packs)
               tp[p.PKEY] = {
                  name: p.TITL,
                  cost: Number(p.PRIC) || 0,
                  slots: prev[p.PKEY]?.slots ?? [],
               };
            tablesRaw = { ...tablesRaw, packs: tp };
            await writeSection("pack_tables", tablesRaw);
         }

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
   <title>Pack List</title>
</svelte:head>

<div class="mx-auto flex max-w-3xl flex-col gap-6 p-6">
   <div class="flex items-center justify-between">
      <div>
         <h1 class="text-2xl font-semibold tracking-tight">Pack List</h1>
      </div>
      <Button variant="outline" onclick={addPack}>
         <PlusIcon class="mr-1 size-4" /> Add pack
      </Button>
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
      {#each packs as pack, i (pack.PKEY)}
         <Card.Root>
            <Card.Content class="flex gap-4 pt-6">
               <!-- reorder -->
               <div class="flex flex-col justify-center gap-1">
                  <Button
                     variant="ghost"
                     size="icon"
                     disabled={i === 0}
                     onclick={() => move(i, -1)}
                  >
                     <ChevronUpIcon class="size-4" />
                  </Button>
                  <span class="text-muted-foreground text-center text-xs"
                     >{i + 1}</span
                  >
                  <Button
                     variant="ghost"
                     size="icon"
                     disabled={i === packs.length - 1}
                     onclick={() => move(i, 1)}
                  >
                     <ChevronDownIcon class="size-4" />
                  </Button>
               </div>

               <img
                  src={packImg(pack.IMGN)}
                  alt=""
                  class="bg-muted size-24 shrink-0 rounded-md object-contain"
               />

               <div class="grid flex-1 gap-2">
                  <div class="flex items-center justify-between">
                     <span class="text-muted-foreground font-mono text-xs"
                        >{pack.PKEY}</span
                     >
                     <Button
                        variant="ghost"
                        size="icon"
                        onclick={() => removePack(i)}
                     >
                        <Trash2Icon class="size-4" />
                     </Button>
                  </div>
                  <Input placeholder="Title" bind:value={pack.TITL} />
                  <Textarea
                     rows={2}
                     placeholder="Description"
                     bind:value={pack.DESC}
                  />
                  <div class="grid grid-cols-2 gap-2">
                     <div class="grid gap-1">
                        <Label class="text-xs">Price ({pack.CONS})</Label>
                        <Input type="number" min="0" bind:value={pack.PRIC} />
                     </div>
                     <div class="grid gap-1">
                        <Label class="text-xs">Badge (ADDT)</Label>
                        <Input
                           placeholder="e.g. TIME LIMITED"
                           bind:value={pack.ADDT}
                        />
                     </div>
                  </div>
                  <div class="grid gap-1">
                     <Label class="text-xs">Image (IMGN)</Label>
                     <ThumbCombobox
                        items={imageOptions}
                        bind:value={pack.IMGN}
                        placeholder="Select an image…"
                        showValue={false}
                     />
                  </div>
               </div>
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
