<script>
  import * as Popover from "$lib/components/ui/popover/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import CheckIcon from "@lucide/svelte/icons/check";
  import ChevronsUpDownIcon from "@lucide/svelte/icons/chevrons-up-down";

  let { items = [], value = $bindable(""), onselect = undefined } = $props();

  let open = $state(false);
  let search = $state("");

  const selectedName = $derived(items.find((i) => i.id === value)?.name ?? "");

  const MAX = 100;
  const filtered = $derived.by(() => {
    const q = search.trim().toLowerCase();
    const base = q
      ? items.filter(
          (i) =>
            i.name.toLowerCase().includes(q) || i.id.toLowerCase().includes(q),
        )
      : items;
    return base.slice(0, MAX);
  });

  function select(id) {
    value = id;
    open = false;
    search = "";
    onselect?.(id);
  }
</script>

<Popover.Root bind:open>
  <Popover.Trigger>
    {#snippet child({ props })}
      <Button
        variant="outline"
        role="combobox"
        class="w-full justify-between"
        {...props}
      >
        <span class="truncate"
          >{selectedName || value || "Select an item…"}</span
        >
        <ChevronsUpDownIcon class="ml-2 size-4 shrink-0 opacity-50" />
      </Button>
    {/snippet}
  </Popover.Trigger>
  <Popover.Content class="w-80 p-0" align="start">
    <div class="border-b p-2">
      <Input placeholder="Search {items.length} items…" bind:value={search} />
    </div>
    <div class="max-h-72 overflow-y-auto py-1">
      {#if filtered.length === 0}
        <p class="text-muted-foreground px-3 py-6 text-center text-sm">
          No items found.
        </p>
      {:else}
        {#each filtered as item (item.id)}
          <button
            type="button"
            class="hover:bg-accent flex w-full items-center gap-2 px-3 py-1.5 text-left text-sm"
            onclick={() => select(item.id)}
          >
            <CheckIcon
              class="size-4 {value === item.id ? 'opacity-100' : 'opacity-0'}"
            />
            <span class="flex-1 truncate">{item.name}</span>
            <span class="text-muted-foreground font-mono text-[10px]"
              >{item.id}</span
            >
          </button>
        {/each}
        {#if !search.trim() && items.length > filtered.length}
          <p class="text-muted-foreground px-3 py-2 text-center text-xs">
            Showing first {MAX} — type to search all {items.length}
          </p>
        {/if}
      {/if}
    </div>
  </Popover.Content>
</Popover.Root>
