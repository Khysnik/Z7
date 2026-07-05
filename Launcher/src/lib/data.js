import { readTextFile, writeTextFile } from "@tauri-apps/plugin-fs";
import { dataDir } from "$lib/paths.js";

// Read-only server catalogs are consolidated into one data/data.json, keyed by
// section (blackmarket, community, packs, pack_tables, extra_licenses, items,
// leaderboards, localization, ...). Writable saves (inventory.json,
// MPProfile.json, bytevault.json) and launcher-only helper files stay separate.

async function dataPath() {
  return `${await dataDir()}\\data.json`;
}

// The whole consolidated catalog.
export async function readData() {
  return JSON.parse(await readTextFile(await dataPath()));
}

// One section, or undefined if it isn't present.
export async function readSection(name) {
  return (await readData())[name];
}

// Replace one section and write the whole catalog back, preserving every other
// section. Re-reads immediately before writing so edits to one section never
// clobber a concurrent edit to another.
export async function writeSection(name, value) {
  const path = await dataPath();
  const data = JSON.parse(await readTextFile(path));
  data[name] = value;
  await writeTextFile(path, JSON.stringify(data, null, 2));
}
