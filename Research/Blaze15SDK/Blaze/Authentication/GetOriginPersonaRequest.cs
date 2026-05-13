using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetOriginPersonaRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeIdOrPersonaName", "mBlazeIdOrPersonaName", 0xD69BF000, TdfType.Union, 0, true), // Tag: UIOP
    ];
    private ITdfMember[] __members;
    private TdfStruct<Blaze15SDK.Blaze.Authentication.OriginPersonaIdentifier?> _blazeIdOrPersonaName = new(__typeInfos[0]);
    public GetOriginPersonaRequest() { __members = [ _blazeIdOrPersonaName ]; }
    public override Tdf CreateNew() => new GetOriginPersonaRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetOriginPersonaRequest";
    public override string GetFullClassName() => "Blaze::Authentication::GetOriginPersonaRequest";
    public Blaze15SDK.Blaze.Authentication.OriginPersonaIdentifier? BlazeIdOrPersonaName { get => _blazeIdOrPersonaName.Value; set => _blazeIdOrPersonaName.Value = value; }
}
