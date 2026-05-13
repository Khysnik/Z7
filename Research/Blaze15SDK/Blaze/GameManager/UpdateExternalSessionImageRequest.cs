using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdateExternalSessionImageRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("CustomImage", "mCustomImage", 0xD69B6700, TdfType.Blob, 1, true), // Tag: UIMG
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfBlob _customImage = new(__typeInfos[1]);

    public UpdateExternalSessionImageRequest()
    {
        __members = [ _gameId, _customImage ];
    }

    public override Tdf CreateNew() => new UpdateExternalSessionImageRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateExternalSessionImageRequest";
    public override string GetFullClassName() => "Blaze::GameManager::UpdateExternalSessionImageRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public byte[] CustomImage
    {
        get => _customImage.Value;
        set => _customImage.Value = value;
    }

}
