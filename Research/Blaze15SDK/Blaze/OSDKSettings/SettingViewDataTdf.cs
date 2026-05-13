using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class SettingViewDataTdf : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Default", "mDefault", 0x9259B300, TdfType.String, 0, true), // Tag: DEFS
        new TdfMemberInfo("Enabled", "mEnabled", 0x96E86200, TdfType.Bool, 1, true), // Tag: ENAB
        new TdfMemberInfo("HelpLabel", "mHelpLabel", 0xA2C86200, TdfType.String, 2, true), // Tag: HLAB
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 3, true), // Tag: ID
        new TdfMemberInfo("Persistent", "mPersistent", 0xC25CB300, TdfType.Bool, 4, true), // Tag: PERS
        new TdfMemberInfo("Value", "mValue", 0xDA1B0000, TdfType.String, 5, true), // Tag: VAL
        new TdfMemberInfo("Visible", "mVisible", 0xDA9CC000, TdfType.Bool, 6, true), // Tag: VIS
    ];
    private ITdfMember[] __members;

    private TdfString _default = new(__typeInfos[0]);
    private TdfBool _enabled = new(__typeInfos[1]);
    private TdfString _helpLabel = new(__typeInfos[2]);
    private TdfString _id = new(__typeInfos[3]);
    private TdfBool _persistent = new(__typeInfos[4]);
    private TdfString _value = new(__typeInfos[5]);
    private TdfBool _visible = new(__typeInfos[6]);

    public SettingViewDataTdf()
    {
        __members = [ _default, _enabled, _helpLabel, _id, _persistent, _value, _visible ];
    }

    public override Tdf CreateNew() => new SettingViewDataTdf();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SettingViewDataTdf";
    public override string GetFullClassName() => "Blaze::OSDKSettings::SettingViewDataTdf";

    public string Default
    {
        get => _default.Value;
        set => _default.Value = value;
    }

    public bool Enabled
    {
        get => _enabled.Value;
        set => _enabled.Value = value;
    }

    public string HelpLabel
    {
        get => _helpLabel.Value;
        set => _helpLabel.Value = value;
    }

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public bool Persistent
    {
        get => _persistent.Value;
        set => _persistent.Value = value;
    }

    public string Value
    {
        get => _value.Value;
        set => _value.Value = value;
    }

    public bool Visible
    {
        get => _visible.Value;
        set => _visible.Value = value;
    }

}
