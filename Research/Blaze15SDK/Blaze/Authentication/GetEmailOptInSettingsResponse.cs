using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetEmailOptInSettingsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EaMayContact", "mEaMayContact", 0x961B6300, TdfType.UInt32, 0, true), // Tag: EAMC
        new TdfMemberInfo("PartnersMayContact", "mPartnersMayContact", 0xC2D8C000, TdfType.UInt32, 1, true), // Tag: PMC
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _eaMayContact = new(__typeInfos[0]);
    private TdfUInt32 _partnersMayContact = new(__typeInfos[1]);

    public GetEmailOptInSettingsResponse()
    {
        __members = [ _eaMayContact, _partnersMayContact ];
    }

    public override Tdf CreateNew() => new GetEmailOptInSettingsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetEmailOptInSettingsResponse";
    public override string GetFullClassName() => "Blaze::Authentication::GetEmailOptInSettingsResponse";

    public uint EaMayContact { get => _eaMayContact.Value; set => _eaMayContact.Value = value; }
    public uint PartnersMayContact { get => _partnersMayContact.Value; set => _partnersMayContact.Value = value; }
}
