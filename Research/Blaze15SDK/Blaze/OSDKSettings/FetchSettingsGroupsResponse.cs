using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class FetchSettingsGroupsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SettingGroupList", "mSettingGroupList", 0xB27CB000, TdfType.List, 0, true), // Tag: LGRP
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.OSDKSettings.SettingGroup> _settingGroupList = new(__typeInfos[0]);

    public FetchSettingsGroupsResponse()
    {
        __members = [ _settingGroupList ];
    }

    public override Tdf CreateNew() => new FetchSettingsGroupsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FetchSettingsGroupsResponse";
    public override string GetFullClassName() => "Blaze::OSDKSettings::FetchSettingsGroupsResponse";

    public IList<Blaze15SDK.Blaze.OSDKSettings.SettingGroup> SettingGroupList
    {
        get => _settingGroupList.Value;
        set => _settingGroupList.Value = value;
    }

}
