import { log } from "./utils/log.js";
import startBytevault from "./servers/bytevault/server.js"
import startEditorial from "./servers/editorial/server.js"
import startRedirector from "./servers/redirector/server.js";
import startQoS from "./servers/QoS/server.js";

log.info("===========================================")
log.info("      Garden Warfare 2 Blaze Server")
log.info("             Version 0.9.9")
log.info("===========================================")

startRedirector(42230, "127.0.0.1", 10041)
startBytevault(42210)
startEditorial(42220)
startQoS(34976, "127.0.0.1", 17499)