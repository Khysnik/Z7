import path from "node:path";
import { fileURLToPath } from "node:url";

export function serverDir(metaUrl, name) {
    const dir = path.dirname(fileURLToPath(metaUrl));
    if (dir.includes("~BUN") || dir.includes("$bunfs")) {
        return path.join(path.dirname(process.execPath), "servers", name);
    }
    return dir;
}
