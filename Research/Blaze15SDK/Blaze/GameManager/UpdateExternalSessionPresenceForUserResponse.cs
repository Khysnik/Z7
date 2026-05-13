using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdateExternalSessionPresenceForUserResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CurrentPrimarySession", "mCurrentPrimarySession", 0x973A6400, TdfType.Struct, 0, true), // Tag: ESID
        new TdfMemberInfo("CurrentPrimaryGameId", "mCurrentPrimaryGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("CurrentPrimary", "mCurrentPrimary", 0xC3397300, TdfType.Struct, 2, true), // Tag: PSES
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD7396900, TdfType.Int64, 3, true), // Tag: USEI
        new TdfMemberInfo("Updated", "mUpdated", 0xD7397300, TdfType.Struct, 4, true), // Tag: USES
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _currentPrimarySession = new(__typeInfos[0]);
    private TdfUInt64 _currentPrimaryGameId = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult?> _currentPrimary = new(__typeInfos[2]);
    private TdfInt64 _blazeId = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult?> _updated = new(__typeInfos[4]);

    public UpdateExternalSessionPresenceForUserResponse()
    {
        __members = [ _currentPrimarySession, _currentPrimaryGameId, _currentPrimary, _blazeId, _updated ];
    }

    public override Tdf CreateNew() => new UpdateExternalSessionPresenceForUserResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateExternalSessionPresenceForUserResponse";
    public override string GetFullClassName() => "Blaze::GameManager::UpdateExternalSessionPresenceForUserResponse";

    public Blaze15SDK.Blaze.ExternalSessionIdentification? CurrentPrimarySession
    {
        get => _currentPrimarySession.Value;
        set => _currentPrimarySession.Value = value;
    }

    public ulong CurrentPrimaryGameId
    {
        get => _currentPrimaryGameId.Value;
        set => _currentPrimaryGameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult? CurrentPrimary
    {
        get => _currentPrimary.Value;
        set => _currentPrimary.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult? Updated
    {
        get => _updated.Value;
        set => _updated.Value = value;
    }

}
