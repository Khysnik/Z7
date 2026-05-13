using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ExternalIdentifiers : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PsnAccountId", "mPsnAccountId", 0xC33A6400, TdfType.UInt64, 0, true), // Tag: PSID
        new TdfMemberInfo("StadiaAccountId", "mStadiaAccountId", 0xCE1A6400, TdfType.UInt64, 1, true), // Tag: SAID
        new TdfMemberInfo("SteamAccountId", "mSteamAccountId", 0xCF4A6400, TdfType.UInt64, 2, true), // Tag: STID
        new TdfMemberInfo("SwitchId", "mSwitchId", 0xCF7A6400, TdfType.String, 3, true), // Tag: SWID
        new TdfMemberInfo("XblAccountId", "mXblAccountId", 0xE22A6400, TdfType.UInt64, 4, true), // Tag: XBID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _psnAccountId = new(__typeInfos[0]);
    private TdfUInt64 _stadiaAccountId = new(__typeInfos[1]);
    private TdfUInt64 _steamAccountId = new(__typeInfos[2]);
    private TdfString _switchId = new(__typeInfos[3]);
    private TdfUInt64 _xblAccountId = new(__typeInfos[4]);

    public ExternalIdentifiers()
    {
        __members = [
            _psnAccountId,
            _stadiaAccountId,
            _steamAccountId,
            _switchId,
            _xblAccountId,
        ];
    }

    public override Tdf CreateNew() => new ExternalIdentifiers();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalIdentifiers";
    public override string GetFullClassName() => "Blaze::ExternalIdentifiers";

    public ulong PsnAccountId
    {
        get => _psnAccountId.Value;
        set => _psnAccountId.Value = value;
    }

    public ulong StadiaAccountId
    {
        get => _stadiaAccountId.Value;
        set => _stadiaAccountId.Value = value;
    }

    public ulong SteamAccountId
    {
        get => _steamAccountId.Value;
        set => _steamAccountId.Value = value;
    }

    public string SwitchId
    {
        get => _switchId.Value;
        set => _switchId.Value = value;
    }

    public ulong XblAccountId
    {
        get => _xblAccountId.Value;
        set => _xblAccountId.Value = value;
    }

}
