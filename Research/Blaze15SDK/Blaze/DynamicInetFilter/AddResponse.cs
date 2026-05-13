using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.DynamicInetFilter;

public class AddResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RowId", "mRowId", 0xCA990000, TdfType.UInt32, 0, true), // Tag: RID
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _rowId = new(__typeInfos[0]);

    public AddResponse()
    {
        __members = [
            _rowId,
        ];
    }

    public override Tdf CreateNew() => new AddResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AddResponse";
    public override string GetFullClassName() => "Blaze::DynamicInetFilter::AddResponse";

    public uint RowId
    {
        get => _rowId.Value;
        set => _rowId.Value = value;
    }

}
