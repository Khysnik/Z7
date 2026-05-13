using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdateExternalSessionPresenceForUserErrorInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ErrorInfo", "mErrorInfo", 0x972CA900, TdfType.Struct, 0, true), // Tag: ERRI
        new TdfMemberInfo("CurrentPrimaryGameId", "mCurrentPrimaryGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("CurrentPrimary", "mCurrentPrimary", 0xC3397300, TdfType.Struct, 2, true), // Tag: PSES
        new TdfMemberInfo("Updated", "mUpdated", 0xD7397300, TdfType.Struct, 3, true), // Tag: USES
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionErrorInfo?> _errorInfo = new(__typeInfos[0]);
    private TdfUInt64 _currentPrimaryGameId = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult?> _currentPrimary = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult?> _updated = new(__typeInfos[3]);

    public UpdateExternalSessionPresenceForUserErrorInfo()
    {
        __members = [ _errorInfo, _currentPrimaryGameId, _currentPrimary, _updated ];
    }

    public override Tdf CreateNew() => new UpdateExternalSessionPresenceForUserErrorInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateExternalSessionPresenceForUserErrorInfo";
    public override string GetFullClassName() => "Blaze::GameManager::UpdateExternalSessionPresenceForUserErrorInfo";

    public Blaze15SDK.Blaze.ExternalSessionErrorInfo? ErrorInfo
    {
        get => _errorInfo.Value;
        set => _errorInfo.Value = value;
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

    public Blaze15SDK.Blaze.GameManager.UpdatedExternalSessionForUserResult? Updated
    {
        get => _updated.Value;
        set => _updated.Value = value;
    }

}
