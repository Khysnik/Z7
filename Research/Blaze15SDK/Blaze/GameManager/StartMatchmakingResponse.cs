using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class StartMatchmakingResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalSessionCorrelationId", "mExternalSessionCorrelationId", 0x8EFA6400, TdfType.String, 0, true), // Tag: COID
        new TdfMemberInfo("ExternalSessionName", "mExternalSessionName", 0x973BAD00, TdfType.String, 1, true), // Tag: ESNM
        new TdfMemberInfo("SessionId", "mSessionId", 0xB73A6400, TdfType.UInt64, 2, true), // Tag: MSID
        new TdfMemberInfo("Scid", "mScid", 0xCE3A6400, TdfType.String, 3, true), // Tag: SCID
        new TdfMemberInfo("ExternalSessionTemplateName", "mExternalSessionTemplateName", 0xCF4B6E00, TdfType.String, 4, true), // Tag: STMN
    ];
    private ITdfMember[] __members;

    private TdfString _externalSessionCorrelationId = new(__typeInfos[0]);
    private TdfString _externalSessionName = new(__typeInfos[1]);
    private TdfUInt64 _sessionId = new(__typeInfos[2]);
    private TdfString _scid = new(__typeInfos[3]);
    private TdfString _externalSessionTemplateName = new(__typeInfos[4]);

    public StartMatchmakingResponse()
    {
        __members = [ _externalSessionCorrelationId, _externalSessionName, _sessionId, _scid, _externalSessionTemplateName ];
    }

    public override Tdf CreateNew() => new StartMatchmakingResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StartMatchmakingResponse";
    public override string GetFullClassName() => "Blaze::GameManager::StartMatchmakingResponse";

    public string ExternalSessionCorrelationId
    {
        get => _externalSessionCorrelationId.Value;
        set => _externalSessionCorrelationId.Value = value;
    }

    public string ExternalSessionName
    {
        get => _externalSessionName.Value;
        set => _externalSessionName.Value = value;
    }

    public ulong SessionId
    {
        get => _sessionId.Value;
        set => _sessionId.Value = value;
    }

    public string Scid
    {
        get => _scid.Value;
        set => _scid.Value = value;
    }

    public string ExternalSessionTemplateName
    {
        get => _externalSessionTemplateName.Value;
        set => _externalSessionTemplateName.Value = value;
    }

}
