namespace Blaze15SDK.Blaze;

public enum GameNetworkTopology : int {
    CLIENT_SERVER_PEER_HOSTED = 0x00,
    CLIENT_SERVER_DEDICATED = 0x01,
    PEER_TO_PEER_FULL_MESH = 0x82,
    NETWORK_DISABLED = 0xFF,
}
