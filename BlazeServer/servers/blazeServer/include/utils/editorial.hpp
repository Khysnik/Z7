#pragma once

#include <string>

namespace gw2::utils {

    // Base URL of the editorial (Node.js) server, which owns the live rotations
    // (Rux, community event/challenge) and the challenge contribution accumulator.
    // Point this at your VPS for a globally synchronized system.
    inline const std::string kEditorialBase = "https://127.0.0.1:42220";

}
