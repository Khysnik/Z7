using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class SettingValue : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Default", "mDefault", 0x92598000, TdfType.String, 0, true), // Tag: DEF
        new TdfMemberInfo("Enabled", "mEnabled", 0x96E86200, TdfType.Bool, 1, true), // Tag: ENAB
        new TdfMemberInfo("HelpLabel", "mHelpLabel", 0xA2C86200, TdfType.String, 2, true), // Tag: HLAB
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 3, true), // Tag: ID
        new TdfMemberInfo("Label", "mLabel", 0xB218AC00, TdfType.String, 4, true), // Tag: LABL
        new TdfMemberInfo("Localized", "mLocalized", 0xB2F8E600, TdfType.Bool, 5, true), // Tag: LOCF
        new TdfMemberInfo("HasLocalizedLabel", "mHasLocalizedLabel", 0xB2F8EC00, TdfType.Bool, 6, true), // Tag: LOCL
        new TdfMemberInfo("HasLocalizedHelpLabel", "mHasLocalizedHelpLabel", 0xB2FA2C00, TdfType.Bool, 7, true), // Tag: LOHL
        new TdfMemberInfo("HasLocalizedValueLabel", "mHasLocalizedValueLabel", 0xB2FDAC00, TdfType.Bool, 8, true), // Tag: LOVL
        new TdfMemberInfo("Persistent", "mPersistent", 0xC25CB300, TdfType.Bool, 9, true), // Tag: PERS
        new TdfMemberInfo("PossibleValues", "mPossibleValues", 0xC3686C00, TdfType.List, 10, true), // Tag: PVAL
        new TdfMemberInfo("PossibleValueLabels", "mPossibleValueLabels", 0xC36B2C00, TdfType.List, 11, true), // Tag: PVLL
        new TdfMemberInfo("Type", "mType", 0xD39C2500, TdfType.String, 12, true), // Tag: TYPE
        new TdfMemberInfo("Visible", "mVisible", 0xDA9CC000, TdfType.Bool, 13, true), // Tag: VIS
    ];
    private ITdfMember[] __members;

    private TdfString _default = new(__typeInfos[0]);
    private TdfBool _enabled = new(__typeInfos[1]);
    private TdfString _helpLabel = new(__typeInfos[2]);
    private TdfString _id = new(__typeInfos[3]);
    private TdfString _label = new(__typeInfos[4]);
    private TdfBool _localized = new(__typeInfos[5]);
    private TdfBool _hasLocalizedLabel = new(__typeInfos[6]);
    private TdfBool _hasLocalizedHelpLabel = new(__typeInfos[7]);
    private TdfBool _hasLocalizedValueLabel = new(__typeInfos[8]);
    private TdfBool _persistent = new(__typeInfos[9]);
    private TdfList<int> _possibleValues = new(__typeInfos[10]);
    private TdfList<string> _possibleValueLabels = new(__typeInfos[11]);
    private TdfString _type = new(__typeInfos[12]);
    private TdfBool _visible = new(__typeInfos[13]);

    public SettingValue()
    {
        __members = [ _default, _enabled, _helpLabel, _id, _label, _localized, _hasLocalizedLabel, _hasLocalizedHelpLabel, _hasLocalizedValueLabel, _persistent, _possibleValues, _possibleValueLabels, _type, _visible ];
    }

    public override Tdf CreateNew() => new SettingValue();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SettingValue";
    public override string GetFullClassName() => "Blaze::OSDKSettings::SettingValue";

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

    public string Label
    {
        get => _label.Value;
        set => _label.Value = value;
    }

    public bool Localized
    {
        get => _localized.Value;
        set => _localized.Value = value;
    }

    public bool HasLocalizedLabel
    {
        get => _hasLocalizedLabel.Value;
        set => _hasLocalizedLabel.Value = value;
    }

    public bool HasLocalizedHelpLabel
    {
        get => _hasLocalizedHelpLabel.Value;
        set => _hasLocalizedHelpLabel.Value = value;
    }

    public bool HasLocalizedValueLabel
    {
        get => _hasLocalizedValueLabel.Value;
        set => _hasLocalizedValueLabel.Value = value;
    }

    public bool Persistent
    {
        get => _persistent.Value;
        set => _persistent.Value = value;
    }

    public IList<int> PossibleValues
    {
        get => _possibleValues.Value;
        set => _possibleValues.Value = value;
    }

    public IList<string> PossibleValueLabels
    {
        get => _possibleValueLabels.Value;
        set => _possibleValueLabels.Value = value;
    }

    public string Type
    {
        get => _type.Value;
        set => _type.Value = value;
    }

    public bool Visible
    {
        get => _visible.Value;
        set => _visible.Value = value;
    }

}
