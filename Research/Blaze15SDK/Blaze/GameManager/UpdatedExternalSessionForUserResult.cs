using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdatedExternalSessionForUserResult : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PsnPushContextId", "mPsnPushContextId", 0xC35CE800, TdfType.String, 0, true), // Tag: PUSH
        new TdfMemberInfo("Activity", "mActivity", 0xD618F400, TdfType.Map, 1, true), // Tag: UACT
        new TdfMemberInfo("GameId", "mGameId", 0xD67A6400, TdfType.UInt64, 2, true), // Tag: UGID
        new TdfMemberInfo("Session", "mSession", 0xD73A6400, TdfType.Struct, 3, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfString _psnPushContextId = new(__typeInfos[0]);
    private TdfMap<Blaze15SDK.Blaze.ExternalSessionActivityType, bool> _activity = new(__typeInfos[1]);
    private TdfUInt64 _gameId = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _session = new(__typeInfos[3]);

    public UpdatedExternalSessionForUserResult()
    {
        __members = [ _psnPushContextId, _activity, _gameId, _session ];
    }

    public override Tdf CreateNew() => new UpdatedExternalSessionForUserResult();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdatedExternalSessionForUserResult";
    public override string GetFullClassName() => "Blaze::GameManager::UpdatedExternalSessionForUserResult";

    public string PsnPushContextId
    {
        get => _psnPushContextId.Value;
        set => _psnPushContextId.Value = value;
    }

    public IDictionary<Blaze15SDK.Blaze.ExternalSessionActivityType, bool> Activity
    {
        get => _activity.Value;
        set => _activity.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionIdentification? Session
    {
        get => _session.Value;
        set => _session.Value = value;
    }

}
