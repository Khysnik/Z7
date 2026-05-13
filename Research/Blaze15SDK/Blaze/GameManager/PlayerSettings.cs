namespace Blaze15SDK.Blaze.GameManager;

[Flags]
public enum PlayerSettings : int
{
    hasJoinFirstPartyGameSessionPermission = 0x1,
    hasDisconnectReservation = 0x2,
}
