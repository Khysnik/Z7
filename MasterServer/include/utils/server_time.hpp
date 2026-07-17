#pragma once

#include <ctime>
#include <cstdint>

namespace gw2 {

inline int64_t toBlazeTime(std::time_t unixSeconds) {
    return unixSeconds;
}

inline int64_t blazeServerNow() {
    return toBlazeTime(std::time(nullptr));
}

} // namespace gw2
