//editorial server, serves static config files and game assets
import { log } from "../../utils/log.js";
import { serverDir } from "../../utils/appdir.js";
import express from "express"
import fs from "node:fs"
import path from "node:path"
import https from "node:https"
import constants from "node:constants";

const __dirname = serverDir(import.meta.url, "editorial");

const app = express();

const sslOptions = {
    cert: fs.readFileSync(path.join(__dirname, "../../certs/server.crt")),
    key: fs.readFileSync(path.join(__dirname, "../../certs/server.key")),
    minVersion: "TLSv1",
    maxVersion: "TLSv1.3",
    ciphers: "DEFAULT:@SECLEVEL=0",
    secureOptions: constants.SSL_OP_NO_SSLv2 | constants.SSL_OP_NO_SSLv3,
};

app.use('/PlantsVsZombies/GW2', express.static(path.join(__dirname, 'files')));

function startEditorial(port) {
    https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
        log.info(`Editorial server started on https://0.0.0.0:${port}`);
    });
}

export default startEditorial