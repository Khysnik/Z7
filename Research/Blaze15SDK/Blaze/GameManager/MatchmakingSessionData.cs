using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MatchmakingSessionData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SessionDuration", "mSessionDuration", 0x935C8000, TdfType.Int64, 0, true), // Tag: DUR
        new TdfMemberInfo("DebugFreezeDecay", "mDebugFreezeDecay", 0x9B2BFA00, TdfType.Bool, 1, true), // Tag: FROZ
        new TdfMemberInfo("SessionMode", "mSessionMode", 0xB6F92500, TdfType.Enum, 2, true), // Tag: MODE
        new TdfMemberInfo("ExternalMmSessionTemplateName", "mExternalMmSessionTemplateName", 0xB73D2E00, TdfType.String, 3, true), // Tag: MSTN
        new TdfMemberInfo("PseudoRequest", "mPseudoRequest", 0xC3392F00, TdfType.Bool, 4, true), // Tag: PSDO
        new TdfMemberInfo("StartingDecayAge", "mStartingDecayAge", 0xCE496300, TdfType.Int64, 5, true), // Tag: SDEC
        new TdfMemberInfo("StartDelay", "mStartDelay", 0xCE496C00, TdfType.Int64, 6, true), // Tag: SDEL
        new TdfMemberInfo("MatchSelf", "mMatchSelf", 0xCE5B2600, TdfType.Bool, 7, true), // Tag: SELF
    ];
    private ITdfMember[] __members;

    private TdfInt64 _sessionDuration = new(__typeInfos[0]);
    private TdfBool _debugFreezeDecay = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.MatchmakingSessionMode> _sessionMode = new(__typeInfos[2]);
    private TdfString _externalMmSessionTemplateName = new(__typeInfos[3]);
    private TdfBool _pseudoRequest = new(__typeInfos[4]);
    private TdfInt64 _startingDecayAge = new(__typeInfos[5]);
    private TdfInt64 _startDelay = new(__typeInfos[6]);
    private TdfBool _matchSelf = new(__typeInfos[7]);

    public MatchmakingSessionData()
    {
        __members = [ _sessionDuration, _debugFreezeDecay, _sessionMode, _externalMmSessionTemplateName, _pseudoRequest, _startingDecayAge, _startDelay, _matchSelf ];
    }

    public override Tdf CreateNew() => new MatchmakingSessionData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MatchmakingSessionData";
    public override string GetFullClassName() => "Blaze::GameManager::MatchmakingSessionData";

    public long SessionDuration
    {
        get => _sessionDuration.Value;
        set => _sessionDuration.Value = value;
    }

    public bool DebugFreezeDecay
    {
        get => _debugFreezeDecay.Value;
        set => _debugFreezeDecay.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingSessionMode SessionMode
    {
        get => _sessionMode.Value;
        set => _sessionMode.Value = value;
    }

    public string ExternalMmSessionTemplateName
    {
        get => _externalMmSessionTemplateName.Value;
        set => _externalMmSessionTemplateName.Value = value;
    }

    public bool PseudoRequest
    {
        get => _pseudoRequest.Value;
        set => _pseudoRequest.Value = value;
    }

    public long StartingDecayAge
    {
        get => _startingDecayAge.Value;
        set => _startingDecayAge.Value = value;
    }

    public long StartDelay
    {
        get => _startDelay.Value;
        set => _startDelay.Value = value;
    }

    public bool MatchSelf
    {
        get => _matchSelf.Value;
        set => _matchSelf.Value = value;
    }

}
