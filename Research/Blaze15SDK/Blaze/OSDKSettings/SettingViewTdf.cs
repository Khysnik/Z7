using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OSDKSettings;

public class SettingViewTdf : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 0, true), // Tag: ID
        new TdfMemberInfo("ViewData", "mViewData", 0xB3693300, TdfType.List, 1, true), // Tag: LVDS
    ];
    private ITdfMember[] __members;

    private TdfString _id = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.OSDKSettings.SettingViewDataTdf> _viewData = new(__typeInfos[1]);

    public SettingViewTdf()
    {
        __members = [ _id, _viewData ];
    }

    public override Tdf CreateNew() => new SettingViewTdf();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SettingViewTdf";
    public override string GetFullClassName() => "Blaze::OSDKSettings::SettingViewTdf";

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public IList<Blaze15SDK.Blaze.OSDKSettings.SettingViewDataTdf> ViewData
    {
        get => _viewData.Value;
        set => _viewData.Value = value;
    }

}
