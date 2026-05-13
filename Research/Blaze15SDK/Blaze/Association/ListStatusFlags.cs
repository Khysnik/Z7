namespace Blaze15SDK.Blaze.Association;

[Flags]
public enum ListStatusFlags : int
{
    Subscribed = 0x1,
    Rollover = 0x2,
    MutualAction = 0x4,
    Paired = 0x8,
    OfflineUED = 0x10,
}
