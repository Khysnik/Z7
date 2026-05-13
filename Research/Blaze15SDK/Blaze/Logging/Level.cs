namespace Blaze15SDK.Blaze.Logging;

[Flags]
public enum Level : int {
    NONE = 0,
    FATAL = 1,
    FAIL = 2,
    ERR = 4,
    WARN = 8,
    INFO = 16,
    TRACE = 32,
    SPAM = 64,
    T_DB = 128,
    T_RPC = 256,
    T_HTTP = 512,
    T_REPL = 1024,
}
