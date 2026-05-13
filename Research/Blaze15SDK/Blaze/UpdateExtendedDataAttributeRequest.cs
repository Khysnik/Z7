using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UpdateExtendedDataAttributeRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Component", "mComponent", 0x8EFB7000, TdfType.UInt16, 0, true), // Tag: COMP
        new TdfMemberInfo("Key", "mKey", 0xAE5E4000, TdfType.UInt16, 1, true), // Tag: KEY
        new TdfMemberInfo("Remove", "mRemove", 0xBF097200, TdfType.Bool, 2, true), // Tag: OPER
        new TdfMemberInfo("Value", "mValue", 0xDA1B3500, TdfType.Int64, 3, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _component = new(__typeInfos[0]);
    private TdfUInt16 _key = new(__typeInfos[1]);
    private TdfBool _remove = new(__typeInfos[2]);
    private TdfInt64 _value = new(__typeInfos[3]);

    public UpdateExtendedDataAttributeRequest()
    {
        __members = [
            _component,
            _key,
            _remove,
            _value,
        ];
    }

    public override Tdf CreateNew() => new UpdateExtendedDataAttributeRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateExtendedDataAttributeRequest";
    public override string GetFullClassName() => "Blaze::UpdateExtendedDataAttributeRequest";

    public ushort Component
    {
        get => _component.Value;
        set => _component.Value = value;
    }

    public ushort Key
    {
        get => _key.Value;
        set => _key.Value = value;
    }

    public bool Remove
    {
        get => _remove.Value;
        set => _remove.Value = value;
    }

    public long Value
    {
        get => _value.Value;
        set => _value.Value = value;
    }

}
