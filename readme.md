# Z7 Project
Z7 is an in-development Blaze server emulator for Plants vs Zombies: Garden Warfare 2. It is made up of 5 individual servers:

- Blaze server - The core game server that handles authentication, loading save data and configs, and matchmaking.
- QoS server - Used to measure your ping and connection speeds to different datacenter locations to find the best one
- Bytevault server - Stores and handles all of your player save data
- Editorial server - Serves static assets and configs
- Redirector server - The initial connection which connects your game to an active Blaze server

All servers except the Blaze server are made in NodeJS as it is a really familiar and simple language to me. The Blaze server on the other hand will be made in C++ as it needs to be a bit more performant than the others.

### Blaze server
Successfully loads to the backyard with player save-data. Save synchronization, save updating, packs, news, matchmaking, events, etc are all TODO.

### QoS server
The HTTP side of this is complete, it just requires the UDP ping server to be built. The Blaze server currently just uses the EA servers

### Bytevault server
In a working state with a static save file, player registration and save editing is TODO.

### Editorial server
Complete.

### Redirector server
Complete.


## Contact me
Discord: **khysnik**, you can find me in the PvZ FB Modding server.

Email: duckie98@protonmail.com

## Credits

- [BlazeSDK](https://github.com/Aim4kill/BlazeSDK) By [@Aim4kill](https://github.com/Aim4kill)
- [ME3PSE](https://github.com/PrivateServerEmulator/ME3PSE) By [@WarrantyVoider](https://github.com/zeroKilo) [@Erik-JS](https://github.com/Erik-JS)
- [BFP4FToolsWV](https://github.com/zeroKilo/BFP4FToolsWV) & [BFP4FToolsWV Wiki](https://github.com/zeroKilo/BFP4FToolsWV/wiki) By [@WarrantyVoider](https://github.com/zeroKilo)
- [PocketRelay](https://github.com/PocketRelay) & [jacobtread/tdf](https://github.com/jacobtread/tdf) By [@jacobtread](https://github.com/jacobtread/)
- [Hall of Meat](https://github.com/hallofmeat) By [@Hall of Meat Team](https://github.com/hallofmeat)
- [BlazeServer](https://github.com/pedromartins1/BlazeServer) By [@Perdo Martins](https://github.com/pedromartins1)
- [BlazeSharkExtended](https://github.com/Tratos/BlazeSharkExtended) By [@Tratos](https://github.com/Tratos)
- [recap_server](https://github.com/vitor251093/recap_server) By [@vitor251093](https://github.com/vitor251093) and [@dalkon](https://github.com/dalkon)
- [openBlase](https://github.com/openBlase/openBlase) By [@openBlase](https://github.com/openBlase/openBlase)
- [BF4BlazeEmulator](https://github.com/buchacho/BF4BlazeEmulator) By [@buchacho](https://github.com/buchacho)
- [@the1Domo](https://github.com/g91)
- [open-ds2-server](https://github.com/lowlevelmetal/open-ds2-server) By [@lowlevelmetal](https://github.com/lowlevelmetal)
- [RaGEZONE Forums](https://forum.ragezone.com/)
- [@BreakfastBrainz2](https://github.com/breakfastbrainz2)
