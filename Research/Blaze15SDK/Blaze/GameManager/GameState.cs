namespace Blaze15SDK.Blaze.GameManager;

public enum GameState : int
{
    NEW_STATE = 0x00,
    INITIALIZING = 0x01,
    INACTIVE_VIRTUAL = 0x02,
    CONNECTION_VERIFICATION = 0x03,
    PRE_GAME = 0x82,
    IN_GAME = 0x83,
    POST_GAME = 0x04,
    MIGRATING = 0x05,
    DESTRUCTING = 0x06,
    RESETABLE = 0x07,
    UNRESPONSIVE = 0x09,
    GAME_GROUP_INITIALIZED = 0x10,
}
