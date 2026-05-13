namespace Blaze15SDK.Blaze;

[Flags]
public enum LookupOpts : int {
    OnlineOnly = 1 << 0,
    MostRecentPlatformOnly = 1 << 1,
    ClientPlatformOnly = 1 << 2,
}
