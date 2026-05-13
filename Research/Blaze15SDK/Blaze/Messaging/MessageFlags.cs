namespace Blaze15SDK.Blaze.Messaging;

[Flags]
public enum MessageFlags : int
{
    Persistent = 0x1,
    Echo = 0x2,
    FilterProfanity = 0x4,
    Localize = 0x8,
    IncludeSelf = 0x10,
    OmitMessageIds = 0x20,
}
