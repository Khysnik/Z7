import { log } from "../../utils/log.js";
import { serverDir } from "../../utils/appdir.js";
import express from "express"
import fs from "node:fs"
import path from "node:path"
import https from "node:https"
import constants from "node:constants";
import * as live from "./GW2/live.js";

const __dirname = serverDir(import.meta.url, "editorial");

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

app.get('/gw2/live/blackmarket', (req, res) => {
    try { 
        res.json(live.blackmarket()); 
    }
    catch (e) { 
        log.error(`live blackmarket: ${e.stack || e}`); 
        res.status(500).json({ error: String(e) }); 
    }
});
app.get('/gw2/live/dailyquests', (req, res) => {
    try { 
        res.json(live.dailyQuests()); 
    }
    catch (e) { 
        log.error(`live dailyquests: ${e.stack || e}`); 
        res.status(500).json({ error: String(e) }); 
    }
});
app.get('/gw2/live/communityevent', (req, res) => {
    try { 
        res.json(live.communityEvent()); 
    }
    catch (e) { 
        log.error(`live communityevent: ${e.stack || e}`); 
        res.status(500).json({ error: String(e) }); 
    }
});
app.get('/gw2/live/communitychallenge', (req, res) => {
    try { 
        res.json(live.communityChallenge(undefined, req.query.user)); 
    }
    catch (e) { 
        log.error(`live communitychallenge: ${e.stack || e}`); 
        res.status(500).json({ error: String(e) }); 
    }
});
app.post('/gw2/live/challenge/contribute', (req, res) => {
    try {
        res.json(live.contributeChallenge(req.body || {}));
    }
    catch (e) {
        log.error(`live contribute: ${e.stack || e}`);
        res.status(500).json({ error: String(e) });
    }
});
app.get('/gw2/live/leaderboard', (req, res) => {
    try {
        res.json(live.leaderboard(req.query.board, Number(req.query.limit) || 100));
    }
    catch (e) {
        log.error(`live leaderboard: ${e.stack || e}`);
        res.status(500).json({ error: String(e) });
    }
});
app.post('/gw2/live/leaderboard/submit', (req, res) => {
    try {
        res.json(live.submitLeaderboard(req.body || {}));
    }
    catch (e) {
        log.error(`live leaderboard submit: ${e.stack || e}`);
        res.status(500).json({ error: String(e) });
    }
});

app.use('/PlantsVsZombies/GW2', express.static(path.join(__dirname, 'GW2')));

app.use('/gameplayservices/prod/PlantsVsZombies/GW3/prod', express.static(path.join(__dirname, 'BFN')));

function startEditorial(port) {
    https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
        log.info(`Editorial server started on https://0.0.0.0:${port}`);
    });
}

export default startEditorial