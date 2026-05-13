using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class SettingGroup : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 0, true), // Tag: ID
        new TdfMemberInfo("Settings", "mSettings", 0xB3397400, TdfType.List, 1, true), // Tag: LSET
        new TdfMemberInfo("Views", "mViews", 0xB36DF300, TdfType.List, 2, true), // Tag: LVWS
    ];
    private ITdfMember[] __members;

    private TdfString _id = new(__typeInfos[0]);
    private TdfList<string> _settings = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.OSDKSettings.SettingViewTdf> _views = new(__typeInfos[2]);

    public SettingGroup()
    {
        __members = [ _id, _settings, _views ];
    }

    public override Tdf CreateNew() => new SettingGroup();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SettingGroup";
    public override string GetFullClassName() => "Blaze::OSDKSettings::SettingGroup";

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public IList<string> Settings
    {
        get => _settings.Value;
        set => _settings.Value = value;
    }

    public IList<Blaze15SDK.Blaze.OSDKSettings.SettingViewTdf> Views
    {
        get => _views.Value;
        set => _views.Value = value;
    }

}
