using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class FilteredUserText : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Result", "mResult", 0x929CB400, TdfType.Enum, 0, true), // Tag: DIRT
        new TdfMemberInfo("FilteredText", "mFilteredText", 0xD74E3400, TdfType.String, 1, true), // Tag: UTXT
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.Util.FilterResult> _result = new(__typeInfos[0]);
    private TdfString _filteredText = new(__typeInfos[1]);

    public FilteredUserText()
    {
        __members = [ _result, _filteredText ];
    }

    public override Tdf CreateNew() => new FilteredUserText();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FilteredUserText";
    public override string GetFullClassName() => "Blaze::GameManager::FilteredUserText";

    public Blaze15SDK.Blaze.Util.FilterResult Result
    {
        get => _result.Value;
        set => _result.Value = value;
    }

    public string FilteredText
    {
        get => _filteredText.Value;
        set => _filteredText.Value = value;
    }

}
