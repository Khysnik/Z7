<script>
	import { Button } from "$lib/components/ui/button/index.js";
	import { HugeiconsIcon } from "@hugeicons/svelte"
	import { SidebarLeftIcon } from '@hugeicons/core-free-icons';
	import { cn } from "$lib/utils.js";
	import { useSidebar } from "./context.svelte.js";

	let {
		ref = $bindable(null),
		class: className,
		onclick,
		...restProps
	} = $props();

	const sidebar = useSidebar();
</script>

<Button
	bind:ref
	data-sidebar="trigger"
	data-slot="sidebar-trigger"
	variant="ghost"
	size="icon-sm"
	class={cn("cn-sidebar-trigger", className)}
	type="button"
	onclick={(e) => {
		onclick?.(e);
		sidebar.toggle();
	}}
	{...restProps}
>
	<HugeiconsIcon icon={SidebarLeftIcon} strokeWidth={2}  />
	<span class="sr-only">Toggle Sidebar</span>
</Button>