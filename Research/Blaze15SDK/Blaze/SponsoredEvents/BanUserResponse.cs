using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class BanUserResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsBanned", "mIsBanned", 0x8A1B8000, TdfType.Int8, 0, true), // Tag: BAN
    ];
    private ITdfMember[] __members;

    private TdfInt8 _isBanned = new(__typeInfos[0]);

    public BanUserResponse()
    {
        __members = [_isBanned];
    }

    public override Tdf CreateNew() => new BanUserResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "BanUserResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::BanUserResponse";

    public sbyte IsBanned
    {
        get => _isBanned.Value;
        set => _isBanned.Value = value;
    }
}
