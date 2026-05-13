using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class UpdateClubSettingsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("ClubSettings", "mClubSettings", 0x8ECCF400, TdfType.Struct, 1, true), // Tag: CLST
        new TdfMemberInfo("TagList", "mTagList", 0x8ECD2700, TdfType.List, 2, true), // Tag: CLTG
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfStruct<ClubSettings?> _clubSettings = new(__typeInfos[1]);
    private TdfList<string> _tagList = new(__typeInfos[2]);

    public UpdateClubSettingsRequest()
    {
        __members = [ _clubId, _clubSettings, _tagList ];
    }

    public override Tdf CreateNew() => new UpdateClubSettingsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateClubSettingsRequest";
    public override string GetFullClassName() => "Blaze::Clubs::UpdateClubSettingsRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public ClubSettings? ClubSettings
    {
        get => _clubSettings.Value;
        set => _clubSettings.Value = value;
    }

    public IList<string> TagList
    {
        get => _tagList.Value;
        set => _tagList.Value = value;
    }

}
