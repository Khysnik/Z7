using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class Lists : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ListMembersVector", "mListMembersVector", 0xB2D87000, TdfType.List, 0, true), // Tag: LMAP
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Association.ListMembers> _listMembersVector = new(__typeInfos[0]);

    public Lists()
    {
        __members = [ _listMembersVector ];
    }

    public override Tdf CreateNew() => new Lists();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Lists";
    public override string GetFullClassName() => "Blaze::Association::Lists";

    public IList<Blaze15SDK.Blaze.Association.ListMembers> ListMembersVector
    {
        get => _listMembersVector.Value;
        set => _listMembersVector.Value = value;
    }

}
