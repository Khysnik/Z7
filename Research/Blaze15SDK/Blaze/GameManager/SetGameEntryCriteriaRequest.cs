using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetGameEntryCriteriaRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntryCriteriaMap", "mEntryCriteriaMap", 0x8F2A7400, TdfType.Map, 0, true), // Tag: CRIT
        new TdfMemberInfo("GameId", "mGameId", 0x9EDA6400, TdfType.UInt64, 1, true), // Tag: GMID
        new TdfMemberInfo("RoleEntryCriteriaMap", "mRoleEntryCriteriaMap", 0xCA3CB400, TdfType.Map, 2, true), // Tag: RCRT
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _entryCriteriaMap = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfMap<string, IDictionary<string, string>> _roleEntryCriteriaMap = new(__typeInfos[2]);

    public SetGameEntryCriteriaRequest()
    {
        __members = [ _entryCriteriaMap, _gameId, _roleEntryCriteriaMap ];
    }

    public override Tdf CreateNew() => new SetGameEntryCriteriaRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetGameEntryCriteriaRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetGameEntryCriteriaRequest";

    public IDictionary<string, string> EntryCriteriaMap
    {
        get => _entryCriteriaMap.Value;
        set => _entryCriteriaMap.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public IDictionary<string, IDictionary<string, string>> RoleEntryCriteriaMap
    {
        get => _roleEntryCriteriaMap.Value;
        set => _roleEntryCriteriaMap.Value = value;
    }

}
