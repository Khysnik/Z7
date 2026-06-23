import { log } from "../../utils/log.js";
import { serverDir } from "../../utils/appdir.js";
import express from "express"
import fs from "node:fs";
import fsp from "node:fs/promises";
import path from "node:path"
import https from "node:https"
import constants from "node:constants";

const __dirname = serverDir(import.meta.url, "bytevault");

const app = express();
app.use(express.json());
const sslOptions = {
    cert: fs.readFileSync(path.join(__dirname, "../../certs/server.crt")),
    key: fs.readFileSync(path.join(__dirname, "../../certs/server.key")),
    minVersion: "TLSv1",
    maxVersion: "TLSv1.3",
    ciphers: "DEFAULT:@SECLEVEL=0",
    secureOptions: constants.SSL_OP_NO_SSLv2 | constants.SSL_OP_NO_SSLv3,
};

const startEpochUs = BigInt(Date.now()) * 1000n;
const startHr = process.hrtime.bigint();

function generateTimestamp() {
    const elapsedUs = (process.hrtime.bigint() - startHr) / 1000n;
    return (startEpochUs + elapsedUs).toString();
}

app.get("/1.0/contexts/plantsvszombies-gw2-pc/categories/PVZ/records/PlayerProfile", (req, res) => {
    let userId = req.query.ownerId

    if (!userId) {
        res.status(401).end()
        return
    }

    let profileData = JSON.parse(fs.readFileSync(path.join(__dirname, `../../data/bytevault.json`))) //temporarily hardcoded

    if (!profileData) {
        res.status(404).end()
        return
    }

    log.info(`[bytevault] Request from uid ${userId}, type=read`)

    res.set('x-blaze-errorcode', 0);
    res.set('x-blaze-component', 'bytevault');
    res.set('x-blaze-command', 'getRecord');
    res.set('x-creation', profileData.creationDate);
    res.set('x-lastupdate', profileData.lastUpdate);
    res.set('server', 'istio-envoy');

    res.json(profileData.data)
});

app.put("/1.0/contexts/plantsvszombies-gw2-pc/categories/PVZ/records/PlayerProfile", async (req, res) => {
    const userId = req.query.ownerId;

    if (!userId) {
        res.status(401).end();
        return;
    }

    log.info(`[bytevault] Request from uid ${userId}, type=write`);

    const filePath = path.join(__dirname, "../../data/bytevault.json");

    try {
        const updates = req.body;

        console.log(updates);

        const fileData = await fsp.readFile(filePath, "utf8");
        const profileData = JSON.parse(fileData);

        for (const [pathString, value] of Object.entries(updates)) {
            const keys = pathString.split(".");

            let current = profileData.data;

            while (keys.length > 1) {
                const key = keys.shift();

                if (!(key in current)) {
                    current[key] = {};
                }
                
                current = current[key];
            }

            current[keys[0]] = value;

        }

        profileData.lastUpdate = generateTimestamp();

        await fsp.writeFile(
            filePath,
            JSON.stringify(profileData, null, 4),
            "utf8"
        );

        res.set("x-blaze-errorcode", 0);
        res.set("x-blaze-component", "bytevault");
        res.set("x-blaze-command", "upsertRecord");
        res.set("x-lastupdate", profileData.lastUpdate);
        res.set("server", "istio-envoy");

        res.json({
            lastUpdateTime: profileData.lastUpdate,
            recordName: "PlayerProfile",
            creationTime: profileData.creationDate,
            owner: {
                type: "NUCLEUS_PERSONA",
                id: userId
            }
        });
    } catch (err) {
        console.error(err);

        res.status(500).json({
            success: false,
            error: err.message
        });
    }
});

function startBytevault(port) {
    https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
        log.info(`Bytevault server started on https://0.0.0.0:${port}`);
    });
}

export default startBytevault