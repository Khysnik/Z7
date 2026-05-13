using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserManagerCensusData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ConnectedPlayerCounts", "mConnectedPlayerCounts", 0x8F08ED00, TdfType.Map, 0, true), // Tag: CPCM
        new TdfMemberInfo("ConnectedPlayerCountByPingSite", "mConnectedPlayerCountByPingSite", 0x8F08F000, TdfType.Map, 1, true), // Tag: CPCP
    ];
    private ITdfMember[] __members;

    private TdfMap<Blaze15SDK.Blaze.ClientType, uint> _connectedPlayerCounts = new(__typeInfos[0]);
    private TdfMap<string, uint> _connectedPlayerCountByPingSite = new(__typeInfos[1]);

    public UserManagerCensusData()
    {
        __members = [
            _connectedPlayerCounts,
            _connectedPlayerCountByPingSite,
        ];
    }

    public override Tdf CreateNew() => new UserManagerCensusData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserManagerCensusData";
    public override string GetFullClassName() => "Blaze::UserManagerCensusData";

    public IDictionary<Blaze15SDK.Blaze.ClientType, uint> ConnectedPlayerCounts
    {
        get => _connectedPlayerCounts.Value;
        set => _connectedPlayerCounts.Value = value;
    }

    public IDictionary<string, uint> ConnectedPlayerCountByPingSite
    {
        get => _connectedPlayerCountByPingSite.Value;
        set => _connectedPlayerCountByPingSite.Value = value;
    }

}
