<script>
    // layout.css (tailwind/shadcn) is kept temporarily for the data pages that
    // haven't been converted yet; theme.css loads after so the plain PvZ styles
    // win. Once every page is plain HTML, layout.css + shadcn go away.
    import "./layout.css";
    import "./theme.css";
    import AppSidebar from "$lib/components/app-sidebar.svelte";

    let { children } = $props();
</script>

<div class="app">
    <AppSidebar />
    <main class="main">
        {@render children?.()}
    </main>
</div>
