using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CommonGameRequestData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DelineationGroup", "mDelineationGroup", 0x925B2700, TdfType.String, 0, true), // Tag: DELG
        new TdfMemberInfo("GameType", "mGameType", 0x9E7D3900, TdfType.Enum, 1, true), // Tag: GGTY
        new TdfMemberInfo("GameProtocolVersionString", "mGameProtocolVersionString", 0x9F697200, TdfType.String, 2, true), // Tag: GVER
        new TdfMemberInfo("PlayerNetworkAddress", "mPlayerNetworkAddress", 0xC2E97400, TdfType.Union, 3, true), // Tag: PNET
    ];
    private ITdfMember[] __members;

    private TdfString _delineationGroup = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameType> _gameType = new(__typeInfos[1]);
    private TdfString _gameProtocolVersionString = new(__typeInfos[2]);
    private TdfUnion<Blaze15SDK.Blaze.NetworkAddress> _playerNetworkAddress = new(__typeInfos[3]);

    public CommonGameRequestData()
    {
        __members = [ _delineationGroup, _gameType, _gameProtocolVersionString, _playerNetworkAddress ];
    }

    public override Tdf CreateNew() => new CommonGameRequestData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CommonGameRequestData";
    public override string GetFullClassName() => "Blaze::GameManager::CommonGameRequestData";

    public string DelineationGroup
    {
        get => _delineationGroup.Value;
        set => _delineationGroup.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameType GameType
    {
        get => _gameType.Value;
        set => _gameType.Value = value;
    }

    public string GameProtocolVersionString
    {
        get => _gameProtocolVersionString.Value;
        set => _gameProtocolVersionString.Value = value;
    }

    public Blaze15SDK.Blaze.NetworkAddress PlayerNetworkAddress
    {
        get => _playerNetworkAddress.Value;
        set => _playerNetworkAddress.Value = value;
    }

}
