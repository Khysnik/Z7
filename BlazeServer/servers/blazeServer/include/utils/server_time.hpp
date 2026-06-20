#pragma once

#include <ctime>
#include <cstdint>

namespace gw2 {

// Blaze server-time fields are plain unix seconds. (The old "* 2" here was a
// band-aid for the TDF integer encoding bug -- the buggy LEB128 codec doubled
// large integers, so captured EA times *looked* like 2x unix. With the codec
// fixed (see src/blaze/tdf.cpp encodeVarInt), raw unix is correct.)
inline int64_t toBlazeTime(std::time_t unixSeconds) {
    return static_cast<int64_t>(unixSeconds);
}

inline int64_t blazeServerNow() {
    return toBlazeTime(std::time(nullptr));
}

} // namespace gw2
