using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class CountMessagesForClubsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CountMap", "mCountMap", 0xB63B7000, TdfType.Map, 0, true), // Tag: MCMP
    ];
    private ITdfMember[] __members;

    private TdfMap<ulong, uint> _countMap = new(__typeInfos[0]);

    public CountMessagesForClubsResponse()
    {
        __members = [ _countMap ];
    }

    public override Tdf CreateNew() => new CountMessagesForClubsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CountMessagesForClubsResponse";
    public override string GetFullClassName() => "Blaze::Clubs::CountMessagesForClubsResponse";

    public IDictionary<ulong, uint> CountMap
    {
        get => _countMap.Value;
        set => _countMap.Value = value;
    }

}
