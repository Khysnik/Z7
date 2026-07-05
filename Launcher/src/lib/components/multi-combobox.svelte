<script>
  import * as Popover from "$lib/components/ui/popover/index.js";
  import { Button } from "$lib/components/ui/button/index.js";
  import { Input } from "$lib/components/ui/input/index.js";
  import CheckIcon from "@lucide/svelte/icons/check";
  import ChevronsUpDownIcon from "@lucide/svelte/icons/chevrons-up-down";

  let {
    items = [],
    values = $bindable([]),
    placeholder = "Select…",
  } = $props();

  let open = $state(false);
  let search = $state("");

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

  const label = $derived(
    values.length === 0
      ? placeholder
      : values
          .map((v) => items.find((i) => i.id === v)?.name ?? v)
          .join(", "),
  );

  function toggle(id) {
    values = values.includes(id)
      ? values.filter((v) => v !== id)
      : [...values, id];
  }
</script>

<Popover.Root bind:open>
  <Popover.Trigger>
    {#snippet child({ props })}
      <Button
        variant="outline"
        role="combobox"
        class="w-full justify-between font-normal"
        {...props}
      >
        <span class="truncate {values.length ? '' : 'text-muted-foreground'}"
          >{label}</span
        >
        <ChevronsUpDownIcon class="ml-2 size-4 shrink-0 opacity-50" />
      </Button>
    {/snippet}
  </Popover.Trigger>
  <Popover.Content class="w-80 p-0" align="start">
    <div class="border-b p-2">
      <Input placeholder="Search {items.length} options…" bind:value={search} />
    </div>
    <div class="max-h-72 overflow-y-auto py-1">
      {#if filtered.length === 0}
        <p class="text-muted-foreground px-3 py-6 text-center text-sm">
          No options found.
        </p>
      {:else}
        {#each filtered as item (item.id)}
          <button
            type="button"
            class="hover:bg-accent flex w-full items-center gap-2 px-3 py-1.5 text-left text-sm"
            onclick={() => toggle(item.id)}
          >
            <CheckIcon
              class="size-4 {values.includes(item.id)
                ? 'opacity-100'
                : 'opacity-0'}"
            />
            <span class="flex-1 truncate">{item.name}</span>
            {#if item.name !== item.id}
              <span class="text-muted-foreground font-mono text-[10px]"
                >{item.id}</span
              >
            {/if}
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
