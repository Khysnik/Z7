using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.XBLSystem;

public class StringList : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("StringstoVerify", "mStringstoVerify", 0xCF4CAC00, TdfType.List, 0, true), // Tag: STRL
    ];
    private ITdfMember[] __members;

    private TdfList<string> _stringstoVerify = new(__typeInfos[0]);

    public StringList()
    {
        __members = [
            _stringstoVerify,
        ];
    }

    public override Tdf CreateNew() => new StringList();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StringList";
    public override string GetFullClassName() => "Blaze::XBLSystem::StringList";

    public IList<string> StringstoVerify
    {
        get => _stringstoVerify.Value;
        set => _stringstoVerify.Value = value;
    }

}
