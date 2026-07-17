<script>
    import { page } from "$app/state";
    import { onlineMode } from "$lib/state.js";

    const home = [{ title: "Home", url: "/" }];

    // `online: true` = server-controlled editor, hidden when Online Mode is on
    // (the config lives on the shared/VPS server, not locally).
    const config = [
        { title: "Black Market", url: "/black-market", online: true },
        { title: "Bytevault", url: "/bytevault" },
        { title: "Community Challenge", url: "/community-challenge", online: true },
        { title: "Community Portal", url: "/community-portal", online: true },
        { title: "Gift Packs", url: "/gift-packs" },
        { title: "Inventory", url: "/inventory" },
        { title: "Killswitches", url: "/killswitches" },
        { title: "Pack List", url: "/pack-list" },
        { title: "Pack Tables", url: "/pack-tables" },
        { title: "Save File", url: "/save-file" },
    ];

    const isActive = (url) => page.url.pathname === url;
</script>

<nav class="sidebar">
    <img class="brand-logo" src="/Z7_SERVERS_LOGO2.webp" alt="Z7 Servers" />

    <div class="group-label">Launcher</div>
    {#each home as item (item.url)}
        <a class="nav-link" class:active={isActive(item.url)} href={item.url}>{item.title}</a>
    {/each}

    <div class="group-label">Config</div>
    {#each config as item (item.url)}
        {#if !(item.online && $onlineMode)}
            <a class="nav-link" class:active={isActive(item.url)} href={item.url}>{item.title}</a>
        {/if}
    {/each}
</nav>
