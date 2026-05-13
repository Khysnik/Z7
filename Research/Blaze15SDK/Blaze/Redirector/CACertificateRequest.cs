using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class CACertificateRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ServiceName", "mServiceName", 0xBA1B6500, TdfType.String, 0, true), // Tag: NAME
        new TdfMemberInfo("Platform", "mPlatform", 0xC2C87400, TdfType.Enum, 1, true), // Tag: PLAT
    ];
    private ITdfMember[] __members;

    private TdfString _serviceName = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[1]);

    public CACertificateRequest()
    {
        __members = [ _serviceName, _platform ];
    }

    public override Tdf CreateNew() => new CACertificateRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CACertificateRequest";
    public override string GetFullClassName() => "Blaze::Redirector::CACertificateRequest";

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

}
