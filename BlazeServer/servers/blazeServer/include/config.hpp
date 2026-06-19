#pragma once

#include <cstdint>
#include <string>

namespace gw2::config {

extern const std::int64_t  blazeId;        // PID / BUID / ID / ORIG / HPID / BLID
extern const std::int64_t  nucleusId;      // UID / XREF / AID / EXID / PIDI
extern const std::int64_t  connGroupId;    // CGID / ULST connection-group object id
extern const std::int64_t  userSessionId;  // HSES / PROS.UID (per-game user session id)
extern const std::int64_t  locale;         // ALOC / LOC

extern const std::string   persona;        // in-game display name
extern const std::string   sessionKey;     // SESS.KEY / PCTK login session key
extern const std::string   email;          // SESS.MAIL
extern const std::string   nasp;           // namespace (NASP)

} // namespace gw2::config
