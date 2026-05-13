namespace Blaze15SDK.Blaze.GameManager;

[Flags]
public enum EvaluateStatus : int
{
    PlayerCountSufficient = 0x1,
    AcceptableHostFound = 0x2,
    TeamSizesSufficient = 0x4,
}
