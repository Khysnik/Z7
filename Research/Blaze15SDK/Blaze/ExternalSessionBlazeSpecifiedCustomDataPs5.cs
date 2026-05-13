using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ExternalSessionBlazeSpecifiedCustomDataPs5 : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("Mode", "mMode", 0x9ED90000, TdfType.String, 1, true), // Tag: GMD
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfString _mode = new(__typeInfos[1]);

    public ExternalSessionBlazeSpecifiedCustomDataPs5()
    {
        __members = [
            _gameId,
            _mode,
        ];
    }

    public override Tdf CreateNew() => new ExternalSessionBlazeSpecifiedCustomDataPs5();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalSessionBlazeSpecifiedCustomDataPs5";
    public override string GetFullClassName() => "Blaze::ExternalSessionBlazeSpecifiedCustomDataPs5";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public string Mode
    {
        get => _mode.Value;
        set => _mode.Value = value;
    }

}
