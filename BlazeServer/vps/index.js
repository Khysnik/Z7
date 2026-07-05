import { log } from "./utils/log.js";
import startEditorial from "./servers/editorial/server.js"

log.info("===========================================")
log.info("      Garden Warfare 2 Blaze Server")
log.info("           Client Sync Server")
log.info("             Version 0.9.9")
log.info("===========================================")

startEditorial(42220)