using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class FetchSettingsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SettingValueList", "mSettingValueList", 0xB33A6E00, TdfType.List, 0, true), // Tag: LSIN
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.OSDKSettings.SettingValue> _settingValueList = new(__typeInfos[0]);

    public FetchSettingsResponse()
    {
        __members = [ _settingValueList ];
    }

    public override Tdf CreateNew() => new FetchSettingsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FetchSettingsResponse";
    public override string GetFullClassName() => "Blaze::OSDKSettings::FetchSettingsResponse";

    public IList<Blaze15SDK.Blaze.OSDKSettings.SettingValue> SettingValueList
    {
        get => _settingValueList.Value;
        set => _settingValueList.Value = value;
    }

}
