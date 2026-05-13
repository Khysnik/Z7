using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_EndGame;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Deaths", "deaths", 0x925D2800, TdfType.UInt16, 0, true), // Tag: DETH
        new TdfMemberInfo("ResultMap", "resultMap", 0xCB3B7000, TdfType.Map, 1, true), // Tag: RSMP
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _deaths = new(__typeInfos[0]);
    private TdfMap<string, ushort> _resultMap = new(__typeInfos[1]);

    public PlayerReport()
    {
        __members = [ _deaths, _resultMap ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_EndGame::PlayerReport";

    public ushort Deaths
    {
        get => _deaths.Value;
        set => _deaths.Value = value;
    }

    public IDictionary<string, ushort> ResultMap
    {
        get => _resultMap.Value;
        set => _resultMap.Value = value;
    }

}
