using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ClientState : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Mode", "mMode", 0xB6F92500, TdfType.Enum, 0, true), // Tag: MODE
        new TdfMemberInfo("Status", "mStatus", 0xCF487400, TdfType.Enum, 1, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.ClientStateMode> _mode = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientStateStatus> _status = new(__typeInfos[1]);

    public ClientState()
    {
        __members = [
            _mode,
            _status,
        ];
    }

    public override Tdf CreateNew() => new ClientState();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClientState";
    public override string GetFullClassName() => "Blaze::ClientState";

    public Blaze15SDK.Blaze.ClientStateMode Mode
    {
        get => _mode.Value;
        set => _mode.Value = value;
    }

    public Blaze15SDK.Blaze.ClientStateStatus Status
    {
        get => _status.Value;
        set => _status.Value = value;
    }

}
