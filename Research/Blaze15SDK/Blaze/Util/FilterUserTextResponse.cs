using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class FilterUserTextResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FilteredTextList", "mFilteredTextList", 0xD2CCF400, TdfType.List, 0, true), // Tag: TLST
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Util.FilteredUserText> _filteredTextList = new(__typeInfos[0]);

    public FilterUserTextResponse()
    {
        __members = [ _filteredTextList ];
    }

    public override Tdf CreateNew() => new FilterUserTextResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FilterUserTextResponse";
    public override string GetFullClassName() => "Blaze::GameManager::FilterUserTextResponse";

    public IList<Blaze15SDK.Blaze.Util.FilteredUserText> FilteredTextList
    {
        get => _filteredTextList.Value;
        set => _filteredTextList.Value = value;
    }

}
