namespace Blaze15SDK.Blaze.GameManager;

[Flags]
public enum PlayerNetConnectionFlags : int
{
    _DEPRECATED = 0x1,
    connectionDemangled = 0x2,
    connectionPktReceived = 0x4,
}
