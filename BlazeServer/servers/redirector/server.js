//redirector server, routes game client to blaze server instance
import { log } from "../../utils/log.js";
import { serverDir } from "../../utils/appdir.js";
import { XMLParser } from "fast-xml-parser";
import express from "express"
import fs from "node:fs"
import path from "node:path"
import https from "node:https"
import constants from "node:constants";

const __dirname = serverDir(import.meta.url, "redirector");

const app = express();
const parser = new XMLParser();

app.use(express.text({ type: '*/*' }));

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
  app.post("/redirector/getServerInstance", (req, res) => {
    const jsonObject = parser.parse(req.body);

    res.set('X-BLAZE-COMPONENT', "redirector");
    res.set('X-BLAZE-COMMAND', "getServerInstance");
    res.set('X-BLAZE-SEQNO', 0);

    res.type('application/xml');

    if (jsonObject.serverinstancerequest.name == "plantsvszombies-gw3-pc") {
      res.send(`<?xml version="1.0" encoding="UTF-8"?>
<serverinstanceinfo>
  <address member="0">
    <valu>
      <hostname>localhost</hostname>
      <ip>${ipv4ToInt(rAddress)}</ip>
      <port>${rPort}</port>
    </valu>
  </address>
  <secure>1</secure>
  <trialservicename></trialservicename>
  <defaultdnsaddress>0</defaultdnsaddress>
  <messages>
    <warnMessage>Z7 Emulator v0.9.9</warnMessage>
  </messages>
</serverinstanceinfo>`)
    } else if (jsonObject.serverinstancerequest.name == "plantsvszombies-gw2-pc") {
      res.send(`<?xml version="1.0" encoding="UTF-8"?>
<serverinstanceinfo>
  <address member="0">
    <valu>
      <hostname>localhost</hostname>
      <ip>${ipv4ToInt(rAddress)}</ip>
      <port>${rPort}</port>
    </valu>
  </address>
  <secure>1</secure>
  <trialservicename></trialservicename>
  <defaultdnsaddress>0</defaultdnsaddress>
  <messages>
    <warnMessage>Z7 Emulator v0.9.9</warnMessage>
  </messages>
</serverinstanceinfo>
`)
    } else {
      log.warn(`[redirector] unknown game client ${jsonObject.serverinstancerequest.name}`)
    }

    log.info(`[redirector] client ${jsonObject.serverinstancerequest.name} ${req.ip}`)

  })

  https.createServer(sslOptions, app).listen(port, "0.0.0.0", () => {
    log.info(`Redirector server started on https://0.0.0.0:${port}`);
  });
}

export default startRedirector