using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Messaging;

public class SendSourceMessageRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Payload", "mPayload", 0xC39B2400, TdfType.Struct, 0, true), // Tag: PYLD
        new TdfMemberInfo("Source", "mSource", 0xCF28E500, TdfType.ObjectId, 1, true), // Tag: SRCE
    ];
    private ITdfMember[] __members;

    private TdfStruct<ClientMessage?> _payload = new(__typeInfos[0]);
    private TdfObjectId _source = new(__typeInfos[1]);

    public SendSourceMessageRequest()
    {
        __members = [ _payload, _source ];
    }

    public override Tdf CreateNew() => new SendSourceMessageRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SendSourceMessageRequest";
    public override string GetFullClassName() => "Blaze::Messaging::SendSourceMessageRequest";

    public ClientMessage? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

    public ObjectId Source
    {
        get => _source.Value;
        set => _source.Value = value;
    }

}
