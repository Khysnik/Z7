using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubLocalizedNews : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AssociateClubId", "mAssociateClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("Text", "mText", 0xBB4E3400, TdfType.String, 1, true), // Tag: NTXT
        new TdfMemberInfo("Flags", "mFlags", 0xBB79AC00, TdfType.Enum, 2, true), // Tag: NWFL
        new TdfMemberInfo("NewsId", "mNewsId", 0xBB7A6400, TdfType.ObjectId, 3, true), // Tag: NWID
        new TdfMemberInfo("Type", "mType", 0xBB7D3900, TdfType.Enum, 4, true), // Tag: NWTY
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD2DCF400, TdfType.UInt32, 5, true), // Tag: TMST
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 6, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _associateClubId = new(__typeInfos[0]);
    private TdfString _text = new(__typeInfos[1]);
    private TdfEnum<ClubNewsFlags> _flags = new(__typeInfos[2]);
    private TdfObjectId _newsId = new(__typeInfos[3]);
    private TdfEnum<NewsType> _type = new(__typeInfos[4]);
    private TdfUInt32 _timestamp = new(__typeInfos[5]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _user = new(__typeInfos[6]);

    public ClubLocalizedNews()
    {
        __members = [ _associateClubId, _text, _flags, _newsId, _type, _timestamp, _user ];
    }

    public override Tdf CreateNew() => new ClubLocalizedNews();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubLocalizedNews";
    public override string GetFullClassName() => "Blaze::Clubs::ClubLocalizedNews";

    public ulong AssociateClubId
    {
        get => _associateClubId.Value;
        set => _associateClubId.Value = value;
    }

    public string Text
    {
        get => _text.Value;
        set => _text.Value = value;
    }

    public ClubNewsFlags Flags
    {
        get => _flags.Value;
        set => _flags.Value = value;
    }

    public ObjectId NewsId
    {
        get => _newsId.Value;
        set => _newsId.Value = value;
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
