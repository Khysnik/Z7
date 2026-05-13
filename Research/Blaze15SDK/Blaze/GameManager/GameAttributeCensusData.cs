using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameAttributeCensusData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AttributeName", "mAttributeName", 0x874D2E00, TdfType.String, 0, true), // Tag: ATTN
        new TdfMemberInfo("AttributeValue", "mAttributeValue", 0x874D3600, TdfType.String, 1, true), // Tag: ATTV
        new TdfMemberInfo("NumOfGames", "mNumOfGames", 0xBAF9A700, TdfType.UInt32, 2, true), // Tag: NOFG
        new TdfMemberInfo("NumOfPlayers", "mNumOfPlayers", 0xBAF9B000, TdfType.UInt32, 3, true), // Tag: NOFP
    ];
    private ITdfMember[] __members;

    private TdfString _attributeName = new(__typeInfos[0]);
    private TdfString _attributeValue = new(__typeInfos[1]);
    private TdfUInt32 _numOfGames = new(__typeInfos[2]);
    private TdfUInt32 _numOfPlayers = new(__typeInfos[3]);

    public GameAttributeCensusData()
    {
        __members = [ _attributeName, _attributeValue, _numOfGames, _numOfPlayers ];
    }

    public override Tdf CreateNew() => new GameAttributeCensusData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributeCensusData";
    public override string GetFullClassName() => "Blaze::GameManager::GameAttributeCensusData";

    public string AttributeName
    {
        get => _attributeName.Value;
        set => _attributeName.Value = value;
    }

    public string AttributeValue
    {
        get => _attributeValue.Value;
        set => _attributeValue.Value = value;
    }

    public uint NumOfGames
    {
        get => _numOfGames.Value;
        set => _numOfGames.Value = value;
    }

    public uint NumOfPlayers
    {
        get => _numOfPlayers.Value;
        set => _numOfPlayers.Value = value;
    }

}
