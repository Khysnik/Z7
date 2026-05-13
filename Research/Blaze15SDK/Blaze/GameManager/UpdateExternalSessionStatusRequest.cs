using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdateExternalSessionStatusRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("ExternalSessionStatus", "mExternalSessionStatus", 0xCF3D2100, TdfType.Struct, 1, true), // Tag: SSTA
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionStatus?> _externalSessionStatus = new(__typeInfos[1]);

    public UpdateExternalSessionStatusRequest()
    {
        __members = [ _gameId, _externalSessionStatus ];
    }

    public override Tdf CreateNew() => new UpdateExternalSessionStatusRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateExternalSessionStatusRequest";
    public override string GetFullClassName() => "Blaze::GameManager::UpdateExternalSessionStatusRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionStatus? ExternalSessionStatus
    {
        get => _externalSessionStatus.Value;
        set => _externalSessionStatus.Value = value;
    }

}
