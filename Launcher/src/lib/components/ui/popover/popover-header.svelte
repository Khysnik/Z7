<script>
	import { cn } from "$lib/utils.js";

	let {
		ref = $bindable(null),
		class: className,
		children,
		...restProps
	} = $props();
</script>

<div
	bind:this={ref}
	data-slot="popover-header"
	class={cn("flex flex-col gap-1 text-xs", className)}
	{...restProps}
>
	{@render children?.()}
</div>