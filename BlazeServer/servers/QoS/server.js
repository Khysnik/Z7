import { log } from "../../utils/log.js";
import { serverDir } from "../../utils/appdir.js";
import express from "express";
import fs from "node:fs";
import path from "node:path";
import https from "node:https";
import dgram from "node:dgram";
import constants from "node:constants";

const __dirname = serverDir(import.meta.url, "QoS");

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

function ipv4Octets(addr) {
    return addr.replace(/^::ffff:/, '').split('.').map(Number);
}

const FW_REQUESTID = 382;
const FW_REQSECRET = 428;


function buildProbeReply(req, rinfo) {
    const reply = Buffer.alloc(30);
    req.copy(reply, 0, 0, 20);
    const oct = ipv4Octets(rinfo.address);
    reply[20] = oct[0];
    reply[21] = oct[1];
    reply[22] = oct[2];
    reply[23] = oct[3];
    reply.writeUInt16BE(rinfo.port & 0xFFFF, 24);
    return reply;
}

function startQosUdp(udpPort, isProbePort) {
    const sock = dgram.createSocket('udp4');
    sock.on('message', (msg, rinfo) => {
        if (isProbePort && msg.length >= 20) {
            sock.send(buildProbeReply(msg, rinfo), rinfo.port, rinfo.address);
            log.info(`[qos] probe from ${rinfo.address}:${rinfo.port}`);
        } else if (msg.length >= 8) {
            log.info(`[qos] firewall probe from ${rinfo.address}:${rinfo.port}`);
        }
    });
    sock.on('error', (err) => log.error(`[qos] UDP error: ${err.message}`));
    sock.bind(udpPort, "0.0.0.0", () => log.info(`[qos] UDP listening on 0.0.0.0:${udpPort}`));
    return sock;
}

function startQoS(httpPort, qosHost = "127.0.0.1", qosUdpPort = 17499) {
    const fwPortA = qosUdpPort + 1;
    const fwPortB = qosUdpPort + 2;
    const qosIpInt = ipv4ToInt(qosHost);

    app.get("/qos/qos", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "qos");
        res.set('X-BLAZE-SEQNO', 0);
        res.type('application/xml');

        if (req.query.qtyp == 2) {
            res.send(`<?xml version="1.0" encoding="UTF-8"?>
<qos>
  <numprobes>10</numprobes>
  <qosport>${qosUdpPort}</qosport>
  <probesize>1200</probesize>
  <qosip>${qosIpInt}</qosip>
  <requestid>1240</requestid>
  <reqsecret>2045</reqsecret>
</qos>
`);
        } else {
            res.send(`<?xml version="1.0" encoding="UTF-8"?>
<qos>
  <numprobes>0</numprobes>
  <qosport>${qosUdpPort}</qosport>
  <probesize>0</probesize>
  <qosip>${qosIpInt}</qosip>
  <requestid>1</requestid>
  <reqsecret>0</reqsecret>
</qos>
`);
        }
        log.info(`[qos] GET /qos/qos qtyp=${req.query.qtyp} from ${req.ip}`);
    });

    app.get("/qos/firewall", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "firewall");
        res.set('X-BLAZE-SEQNO', 0);
        res.type('application/xml');

        res.send(`<?xml version="1.0" encoding="UTF-8"?>
<firewall>
  <ips>
    <ips>${qosIpInt}</ips>
    <ips>${qosIpInt}</ips>
  </ips>
  <numinterfaces>2</numinterfaces>
  <ports>
    <ports>${fwPortA}</ports>
    <ports>${fwPortB}</ports>
  </ports>
  <requestid>${FW_REQUESTID}</requestid>
  <reqsecret>${FW_REQSECRET}</reqsecret>
</firewall>
`);
        log.info(`[qos] GET /qos/firewall from ${req.ip}`);
    });

    app.get("/qos/firetype", (req, res) => {
        res.set('X-BLAZE-COMPONENT', "qos");
        res.set('X-BLAZE-COMMAND', "firetype");
        res.set('X-BLAZE-SEQNO', 0);
        res.type('application/xml');

        res.send(`<?xml version="1.0" encoding="UTF-8"?>
<firetype>
  <firetype>4</firetype>
</firetype>
`);
        log.info(`[qos] GET /qos/firetype from ${req.ip}`);
    });

    startQosUdp(qosUdpPort, true);
    startQosUdp(fwPortA, false);
    startQosUdp(fwPortB, false);

    https.createServer(sslOptions, app).listen(httpPort, "0.0.0.0", () => {
        log.info(`QoS server started on https://0.0.0.0:${httpPort}`);
    });
}

export default startQoS;
