//bytevault server, handles player save data
import { log } from "../../utils/log.js";
import { fileURLToPath } from 'node:url';
import express from "express"
import fs from "node:fs"
import path from "node:path"
import https from "node:https"
import constants from "node:constants";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();

const sslOptions = {
    cert: fs.readFileSync(path.join(__dirname, "../../certs/server.crt")),
    key: fs.readFileSync(path.join(__dirname, "../../certs/server.key")),
    minVersion: "TLSv1",
    maxVersion: "TLSv1.3",
    ciphers: "DEFAULT:@SECLEVEL=0",
    secureOptions: constants.SSL_OP_NO_SSLv2 | constants.SSL_OP_NO_SSLv3,
};

app.get("/1.0/contexts/plantsvszombies-gw2-pc/categories/PVZ/records/PlayerProfile", (req, res) => {
    let userId = req.query.ownerId
    let write = req.query.paramssubrecordupdate == true ? 1 : 0

    if(!userId){
        res.status(401).end()
        return
    }

    let profileData = JSON.parse(fs.readFileSync(path.join(__dirname, `./db/1006900723798.json`))) //temporarily hardcoded

    if(!profileData) {
        res.status(404).end()
        return
    }

    if (write) {
        log.info(`[bytevault] Request from uid ${userId}, type=write`)

        let update = generateTimestamp()

        res.set('x-blaze-errorcode', 0);
        res.set('x-blaze-component', bytevault);
        res.set('x-blaze-command', upsertRecord);
        res.set('x-lastupdate', update);
        res.set('server', 'istio-envoy');

        res.json({
            "lastUpdateTime": update,
            "recordName": "PlayerProfile",
            "creationTime": profileData.creationDate,
            "owner": {
                "type": "NUCLEUS_PERSONA",
                "id": userId
            }
        })
    } else {
        log.info(`[bytevault] Request from uid ${userId}, type=read`)

        res.set('x-blaze-errorcode', 0);
        res.set('x-blaze-component', 'bytevault');
        res.set('x-blaze-command', 'getRecord');
        res.set('x-creation', profileData.creationDate);
        res.set('x-lastupdate', profileData.lastUpdate);
        res.set('server', 'istio-envoy');

        res.json(profileData.data)
    }
});

function startBytevault(port) {
    https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
        log.info(`Bytevault server started on https://0.0.0.0:${port}`);
    });
}

export default startBytevault