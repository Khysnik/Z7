#include "config.hpp"

// Hardcoded user info, will eventually move to a json file but i'm `l a z y`

namespace gw2::config {

std::int64_t        blazeId        = 2013801447574;   // -pid
std::int64_t        nucleusId      = 2029502247574;   // -nid
const std::int64_t  connGroupId    = 2785761260933;
const std::int64_t  userSessionId  = 2788761260933;
const std::int64_t  locale         = 3403459219;   // "enUS"

std::string         persona        = "Player";   // -name
const std::string   sessionKey     = "00000122a73fe9c4_mldg403m4ntIBSmrM2WUk5fQ3OUJisP4yQTmWofRQz";
const std::string   email          = "******@outlook.com";
const std::string   nasp           = "cem_ea_id";

bool                disableCommunityChallenge = false;   // -disableCC
bool                disableCommunityPortal    = false;   // -disableCP

} // namespace gw2::config
