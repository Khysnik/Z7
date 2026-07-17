#pragma once

#include <cstdint>
#include <string>

namespace gw2::config {

extern std::int64_t        blazeId;        // PID / BUID / ID / ORIG / HPID / BLID (overridable via -pid)
extern std::int64_t        nucleusId;      // UID / XREF / AID / EXID / PIDI (overridable via -nid)
extern const std::int64_t  connGroupId;    // CGID / ULST connection-group object id
extern const std::int64_t  userSessionId;  // HSES / PROS.UID (per-game user session id)
extern const std::int64_t  locale;         // ALOC / LOC

extern const std::int64_t  gameServerIp;   // dedicated gameserver IP (big-endian uint32) matchmaking dials
extern const std::int64_t  gameServerPort; // dedicated gameserver UDP port

extern std::string         persona;        // in-game display name (overridable via -name)
extern const std::string   sessionKey;     // SESS.KEY / PCTK login session key
extern const std::string   email;          // SESS.MAIL
extern const std::string   nasp;           // namespace (NASP)

extern bool                disableCommunityChallenge;  // -disableCC : reply empty to getCommunityAchievements
extern bool                disableCommunityPortal;     // -disableCP : reply empty to getCommunityPortalData
extern bool                onlineMode;                 // set by -online : use the (shared) editorial server for
                                                       // live data. Offline uses the local data.json sections the
                                                       // launcher edits directly.

} // namespace gw2::config
