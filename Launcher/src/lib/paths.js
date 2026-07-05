import { invoke } from "@tauri-apps/api/core";

// The launcher exe's own folder (resolved once at runtime). Everything —
// data/, courgette.exe, the patches, the servers — lives alongside it.
let _base;
export async function baseDir() {
 if (_base == null) _base = await invoke("base_dir");
 return _base;
}

export async function dataDir() {
 return `${await baseDir()}\\data`;
}

export async function launcherDir() {
 return `${await dataDir()}\\launcher`;
}
