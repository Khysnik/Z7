#pragma once

#include <ctime>
#include <cstdint>

namespace gw2 {

inline int64_t toBlazeTime(std::time_t unixSeconds) {
    return static_cast<int64_t>(unixSeconds) * 2;
}

inline int64_t blazeServerNow() {
    return toBlazeTime(std::time(nullptr));
}

} // namespace gw2
