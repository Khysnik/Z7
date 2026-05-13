using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.XBLSystem;

public class Result : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ResultCode", "mResultCode", 0x8EF92500, TdfType.UInt16, 0, true), // Tag: CODE
        new TdfMemberInfo("OffendingString", "mOffendingString", 0xBE69B300, TdfType.String, 1, true), // Tag: OFFS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _resultCode = new(__typeInfos[0]);
    private TdfString _offendingString = new(__typeInfos[1]);

    public Result()
    {
        __members = [
            _resultCode,
            _offendingString,
        ];
    }

    public override Tdf CreateNew() => new Result();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Result";
    public override string GetFullClassName() => "Blaze::XBLSystem::Result";

    public ushort ResultCode
    {
        get => _resultCode.Value;
        set => _resultCode.Value = value;
    }

    public string OffendingString
    {
        get => _offendingString.Value;
        set => _offendingString.Value = value;
    }

}
