//take command line args and start all servers
import { log } from "./utils/log.js";
import startBytevault from "./servers/bytevault/server.js"
import startEditorial from "./servers/editorial/server.js"
import startRedirector from "./servers/redirector/server.js";

log.info("===========================================")
log.info("      Garden Warfare 2 Blaze Server")
log.info("             Version 0.1.0")
log.info("===========================================")

startRedirector(42230, "127.0.0.1", 10041)
startBytevault(42210)
startEditorial(443)