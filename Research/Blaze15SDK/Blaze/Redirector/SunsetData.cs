using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class SunsetData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DownDate", "mDownDate", 0x92487400, TdfType.String, 0, true), // Tag: DDAT
        new TdfMemberInfo("DownMessage", "mDownMessage", 0x92DCE700, TdfType.String, 1, true), // Tag: DMSG
        new TdfMemberInfo("WarnDate", "mWarnDate", 0xDE487400, TdfType.String, 2, true), // Tag: WDAT
        new TdfMemberInfo("WarnMessage", "mWarnMessage", 0xDEDCE700, TdfType.String, 3, true), // Tag: WMSG
    ];
    private ITdfMember[] __members;

    private TdfString _downDate = new(__typeInfos[0]);
    private TdfString _downMessage = new(__typeInfos[1]);
    private TdfString _warnDate = new(__typeInfos[2]);
    private TdfString _warnMessage = new(__typeInfos[3]);

    public SunsetData()
    {
        __members = [ _downDate, _downMessage, _warnDate, _warnMessage ];
    }

    public override Tdf CreateNew() => new SunsetData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SunsetData";
    public override string GetFullClassName() => "Blaze::Redirector::SunsetData";

    public string DownDate
    {
        get => _downDate.Value;
        set => _downDate.Value = value;
    }

    public string DownMessage
    {
        get => _downMessage.Value;
        set => _downMessage.Value = value;
    }

    public string WarnDate
    {
        get => _warnDate.Value;
        set => _warnDate.Value = value;
    }

    public string WarnMessage
    {
        get => _warnMessage.Value;
        set => _warnMessage.Value = value;
    }

}
