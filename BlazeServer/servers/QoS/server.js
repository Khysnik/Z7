//redirector server, routes game client to blaze server instance
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

function ipv4ToInt(ip) {
    const buf = Buffer.from(ip.split('.').map(Number));
    return buf.readUInt32BE(0);
}


function startRedirector(port, rAddress, rPort) {
    app.get("/qos/qos", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "qos");
        res.set('X-BLAZE-SEQNO', 0);

        res.type('application/xml');

        if (req.query.qtyp == 1) {

            res.send(`<?xml version="1.0" encoding="UTF-8"?>
<qos>
  <numprobes>0</numprobes>
  <qosport>${rPort}</qosport>
  <probesize>0</probesize>
  <qosip>${ipv4ToInt(rAddress)}</qosip>
  <requestid>1</requestid>
  <reqsecret>0</reqsecret>
</qos>
`);
        } else if (req.query.qtyp == 2) {
            res.send(`<?xml version="1.0" encoding="UTF-8"?>
<qos>
  <numprobes>10</numprobes>
  <qosport>${rPort}</qosport>
  <probesize>1200</probesize>
  <qosip>${ipv4ToInt(rAddress)}</qosip>
  <requestid>1240</requestid>
  <reqsecret>2045</reqsecret>
</qos>
`)
        }
        log.info(`[qos] GET /qos/qos from ${req.ip}`)

    })

    app.get("/qos/firewall", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "firewall");
        res.set('X-BLAZE-SEQNO', 0);

        res.type('application/xml');

        res.send(`<?xml version="1.0" encoding="UTF-8"?>
<firewall>
  <ips>
    <ips>${ipv4ToInt(rAddress)}</ips>
    <ips>${ipv4ToInt(rAddress)}</ips>
  </ips>
  <numinterfaces>2</numinterfaces>
  <ports>
    <ports>${rPort}</ports>
    <ports>${rPort}</ports>
  </ports>
  <requestid>382</requestid>
  <reqsecret>428</reqsecret>
</firewall>
`)
        log.info(`[qos] GET /qos/firewall from ${req.ip}`)
    })

    app.get("/qos/firetype", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "firetype");
        res.set('X-BLAZE-SEQNO', 0);

        res.type('application/xml');

        res.send(`<?xml version="1.0" encoding="UTF-8"?>
<firetype>
  <firetype>4</firetype>
</firetype>
`)
        log.info(`[qos] GET /qos/firetype from ${req.ip}`)
    })


    https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
        log.info(`Redirector server started on https://0.0.0.0:${port}`);
    });
}

export default startRedirector