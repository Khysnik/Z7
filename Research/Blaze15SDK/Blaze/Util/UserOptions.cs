using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class UserOptions : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TelemetryOpt", "mTelemetryOpt", 0xD2DBF000, TdfType.Enum, 0, true), // Tag: TMOP
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.Util.TelemetryOpt> _telemetryOpt = new(__typeInfos[0]);
    private TdfInt64 _blazeId = new(__typeInfos[1]);

    public UserOptions()
    {
        __members = [ _telemetryOpt, _blazeId ];
    }

    public override Tdf CreateNew() => new UserOptions();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserOptions";
    public override string GetFullClassName() => "Blaze::GameManager::UserOptions";

    public Blaze15SDK.Blaze.Util.TelemetryOpt TelemetryOpt
    {
        get => _telemetryOpt.Value;
        set => _telemetryOpt.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
