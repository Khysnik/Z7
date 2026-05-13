using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class CACertificateResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CertificateList", "mCertificateList", 0x8E5CB400, TdfType.List, 0, true), // Tag: CERT
    ];
    private ITdfMember[] __members;

    private TdfList<byte[]> _certificateList = new(__typeInfos[0]);

    public CACertificateResponse()
    {
        __members = [ _certificateList ];
    }

    public override Tdf CreateNew() => new CACertificateResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CACertificateResponse";
    public override string GetFullClassName() => "Blaze::Redirector::CACertificateResponse";

    public IList<byte[]> CertificateList
    {
        get => _certificateList.Value;
        set => _certificateList.Value = value;
    }

}
