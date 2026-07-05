<script>
 import * as Popover from "$lib/components/ui/popover/index.js";
 import { Button } from "$lib/components/ui/button/index.js";
 import { Input } from "$lib/components/ui/input/index.js";
 import ChevronsUpDownIcon from "@lucide/svelte/icons/chevrons-up-down";

 // `items` is [{ value, label, img }]; `value` is the selected value (bindable).
 // `onselect` fires with the chosen value (useful for "add to list" usage).
 // `search` enables a filter box (auto-on when there are many items).
 let {
  items = [],
  value = $bindable(""),
  placeholder = "Select…",
  onselect = undefined,
  search = undefined,
  showValue = true,
 } = $props();

 let open = $state(false);
 let query = $state("");
 const selected = $derived(items.find((i) => i.value === value));
 const showSearch = $derived(search ?? items.length > 12);
 const filtered = $derived.by(() => {
  const q = query.trim().toLowerCase();
  const base = q
   ? items.filter(
      (i) =>
       i.label.toLowerCase().includes(q) ||
       String(i.value).toLowerCase().includes(q)
     )
   : items;
  return base.slice(0, 200);
 });
</script>

<Popover.Root bind:open>
 <Popover.Trigger>
  {#snippet child({ props })}
   <Button variant="outline" class="h-auto w-full justify-between py-2" {...props}>
    <span class="flex min-w-0 items-center gap-2">
     {#if selected?.img}
      <img src={selected.img} alt="" class="size-9 shrink-0 rounded object-contain" />
     {/if}
     <span class="truncate">{selected?.label ?? value ?? placeholder}</span>
    </span>
    <ChevronsUpDownIcon class="ml-2 size-4 shrink-0 opacity-50" />
   </Button>
  {/snippet}
 </Popover.Trigger>
 <Popover.Content class="w-80 p-1" align="start">
  {#if showSearch}
   <div class="p-1">
    <Input placeholder="Search {items.length}…" bind:value={query} />
   </div>
  {/if}
  <div class="max-h-80 overflow-y-auto">
   {#if filtered.length === 0}
    <p class="text-muted-foreground px-3 py-6 text-center text-sm">Nothing to show.</p>
   {:else}
    {#each filtered as item (item.value)}
     <button
      type="button"
      class="hover:bg-accent flex w-full items-center gap-2 rounded px-2 py-1.5 text-left text-sm"
      onclick={() => {
       value = item.value;
       open = false;
       onselect?.(item.value);
      }}
     >
      {#if item.img}
       <img src={item.img} alt="" class="size-10 shrink-0 rounded object-contain" />
      {/if}
      <span class="flex-1 truncate">{item.label}</span>
      {#if showValue}
       <span class="text-muted-foreground font-mono text-[10px]">{item.value}</span>
      {/if}
     </button>
    {/each}
   {/if}
  </div>
 </Popover.Content>
</Popover.Root>
