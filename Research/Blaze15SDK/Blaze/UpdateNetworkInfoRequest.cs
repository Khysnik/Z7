using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UpdateNetworkInfoRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NetworkInfo", "mNetworkInfo", 0xA6E9AF00, TdfType.Struct, 0, true), // Tag: INFO
        new TdfMemberInfo("Opts", "mOpts", 0xBF0D3300, TdfType.Enum, 1, true), // Tag: OPTS
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.NetworkInfo?> _networkInfo = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.UpdateNetworkInfoOpts> _opts = new(__typeInfos[1]);

    public UpdateNetworkInfoRequest()
    {
        __members = [
            _networkInfo,
            _opts,
        ];
    }

    public override Tdf CreateNew() => new UpdateNetworkInfoRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateNetworkInfoRequest";
    public override string GetFullClassName() => "Blaze::UpdateNetworkInfoRequest";

    public Blaze15SDK.Blaze.NetworkInfo? NetworkInfo
    {
        get => _networkInfo.Value;
        set => _networkInfo.Value = value;
    }

    public Blaze15SDK.Blaze.UpdateNetworkInfoOpts Opts
    {
        get => _opts.Value;
        set => _opts.Value = value;
    }

}
