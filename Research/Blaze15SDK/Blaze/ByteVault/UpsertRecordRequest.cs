using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertRecordRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RecordAddress", "mRecordAddress", 0x86493200, TdfType.Struct, 0, true), // Tag: ADDR
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 1, true), // Tag: AUTH
        new TdfMemberInfo("Payload", "mPayload", 0xB2F86400, TdfType.Struct, 2, true), // Tag: LOAD
        new TdfMemberInfo("SubrecordUpdate", "mSubrecordUpdate", 0xCF58B200, TdfType.Bool, 3, true), // Tag: SUBR
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.RecordAddress?> _recordAddress = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.RecordPayload?> _payload = new(__typeInfos[2]);
    private TdfBool _subrecordUpdate = new(__typeInfos[3]);

    public UpsertRecordRequest()
    {
        __members = [ _recordAddress, _authCredentials, _payload, _subrecordUpdate ];
    }

    public override Tdf CreateNew() => new UpsertRecordRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertRecordRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertRecordRequest";

    public Blaze15SDK.Blaze.ByteVault.RecordAddress? RecordAddress
    {
        get => _recordAddress.Value;
        set => _recordAddress.Value = value;
    }

    public Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials? AuthCredentials
    {
        get => _authCredentials.Value;
        set => _authCredentials.Value = value;
    }

    public Blaze15SDK.Blaze.ByteVault.RecordPayload? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

    public bool SubrecordUpdate
    {
        get => _subrecordUpdate.Value;
        set => _subrecordUpdate.Value = value;
    }

}
