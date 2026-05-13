using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UpdateUserSessionAttributeRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Key", "mKey", 0x874A6400, TdfType.UInt64, 0, true), // Tag: ATID
        new TdfMemberInfo("Remove", "mRemove", 0xBF097200, TdfType.Bool, 1, true), // Tag: OPER
        new TdfMemberInfo("UpdateReason", "mUpdateReason", 0xD7296100, TdfType.UInt32, 2, true), // Tag: UREA
        new TdfMemberInfo("Value", "mValue", 0xDA1B3500, TdfType.UInt64, 3, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _key = new(__typeInfos[0]);
    private TdfBool _remove = new(__typeInfos[1]);
    private TdfUInt32 _updateReason = new(__typeInfos[2]);
    private TdfUInt64 _value = new(__typeInfos[3]);

    public UpdateUserSessionAttributeRequest()
    {
        __members = [
            _key,
            _remove,
            _updateReason,
            _value,
        ];
    }

    public override Tdf CreateNew() => new UpdateUserSessionAttributeRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateUserSessionAttributeRequest";
    public override string GetFullClassName() => "Blaze::UpdateUserSessionAttributeRequest";

    public ulong Key
    {
        get => _key.Value;
        set => _key.Value = value;
    }

    public bool Remove
    {
        get => _remove.Value;
        set => _remove.Value = value;
    }

    public uint UpdateReason
    {
        get => _updateReason.Value;
        set => _updateReason.Value = value;
    }

    public ulong Value
    {
        get => _value.Value;
        set => _value.Value = value;
    }

}
