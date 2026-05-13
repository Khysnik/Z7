namespace Blaze15SDK.Blaze.GameManager;

public enum PlayerState : int
{
    RESERVED,
    QUEUED,
    ACTIVE_CONNECTING,
    ACTIVE_MIGRATING,
    ACTIVE_CONNECTED,
    ACTIVE_KICK_PENDING,
}
