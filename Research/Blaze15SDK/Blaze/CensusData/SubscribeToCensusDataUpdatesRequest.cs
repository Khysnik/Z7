using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.CensusData;

public class SubscribeToCensusDataUpdatesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Resubscribe", "mResubscribe", 0xCB3D6200, TdfType.Bool, 0, true), // Tag: RSUB
    ];
    private ITdfMember[] __members;

    private TdfBool _resubscribe = new(__typeInfos[0]);

    public SubscribeToCensusDataUpdatesRequest()
    {
        __members = [ _resubscribe ];
    }

    public override Tdf CreateNew() => new SubscribeToCensusDataUpdatesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SubscribeToCensusDataUpdatesRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SubscribeToCensusDataUpdatesRequest";

    public bool Resubscribe
    {
        get => _resubscribe.Value;
        set => _resubscribe.Value = value;
    }

}
