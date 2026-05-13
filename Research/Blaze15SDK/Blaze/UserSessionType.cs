namespace Blaze15SDK.Blaze;

public enum UserSessionType : int {
    USER_SESSION_NORMAL = 0x0,
    USER_SESSION_GUEST = 0x1,
    USER_SESSION_WAL_CLONE = 0x2,
    USER_SESSION_TRUSTED = 0x3,
}
