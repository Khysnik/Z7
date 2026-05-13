using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubNews : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AssociateClubId", "mAssociateClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("ParamList", "mParamList", 0xBB0CAC00, TdfType.String, 1, true), // Tag: NPRL
        new TdfMemberInfo("StringId", "mStringId", 0xBB3A7300, TdfType.String, 2, true), // Tag: NSIS
        new TdfMemberInfo("Text", "mText", 0xBB4E3400, TdfType.String, 3, true), // Tag: NTXT
        new TdfMemberInfo("Type", "mType", 0xBB7D3900, TdfType.Enum, 4, true), // Tag: NWTY
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD2DCF400, TdfType.UInt32, 5, true), // Tag: TMST
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 6, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _associateClubId = new(__typeInfos[0]);
    private TdfString _paramList = new(__typeInfos[1]);
    private TdfString _stringId = new(__typeInfos[2]);
    private TdfString _text = new(__typeInfos[3]);
    private TdfEnum<NewsType> _type = new(__typeInfos[4]);
    private TdfUInt32 _timestamp = new(__typeInfos[5]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _user = new(__typeInfos[6]);

    public ClubNews()
    {
        __members = [ _associateClubId, _paramList, _stringId, _text, _type, _timestamp, _user ];
    }

    public override Tdf CreateNew() => new ClubNews();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubNews";
    public override string GetFullClassName() => "Blaze::Clubs::ClubNews";

    public ulong AssociateClubId
    {
        get => _associateClubId.Value;
        set => _associateClubId.Value = value;
    }

    public string ParamList
    {
        get => _paramList.Value;
        set => _paramList.Value = value;
    }

    public string StringId
    {
        get => _stringId.Value;
        set => _stringId.Value = value;
    }

    public string Text
    {
        get => _text.Value;
        set => _text.Value = value;
    }

    public NewsType Type
    {
        get => _type.Value;
        set => _type.Value = value;
    }

    public uint Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
