namespace Blaze15SDK.Blaze.Messaging;

[Flags]
public enum MatchFlags : int
{
    MatchId = 0x1,
    MatchSource = 0x2,
    MatchType = 0x4,
    MatchTarget = 0x8,
    MatchStatus = 0x10,
    MatchTypes = 0x20,
    NotMatchTypes = 0x40,
}
