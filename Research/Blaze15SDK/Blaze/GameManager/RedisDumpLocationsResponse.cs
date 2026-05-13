using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class RedisDumpLocationsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DumpLocations", "mDumpLocations", 0x92DC2C00, TdfType.Map, 0, true), // Tag: DMPL
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _dumpLocations = new(__typeInfos[0]);

    public RedisDumpLocationsResponse()
    {
        __members = [ _dumpLocations ];
    }

    public override Tdf CreateNew() => new RedisDumpLocationsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RedisDumpLocationsResponse";
    public override string GetFullClassName() => "Blaze::GameManager::RedisDumpLocationsResponse";

    public IDictionary<string, string> DumpLocations
    {
        get => _dumpLocations.Value;
        set => _dumpLocations.Value = value;
    }

}
