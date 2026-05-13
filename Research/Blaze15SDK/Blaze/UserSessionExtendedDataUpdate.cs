using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserSessionExtendedDataUpdate : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExtendedData", "mExtendedData", 0x921D2100, TdfType.Struct, 0, true), // Tag: DATA
        new TdfMemberInfo("Subscribed", "mSubscribed", 0xCF58B300, TdfType.Bool, 1, true), // Tag: SUBS
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD73A6400, TdfType.Int64, 2, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.UserSessionExtendedData?> _extendedData = new(__typeInfos[0]);
    private TdfBool _subscribed = new(__typeInfos[1]);
    private TdfInt64 _blazeId = new(__typeInfos[2]);

    public UserSessionExtendedDataUpdate()
    {
        __members = [
            _extendedData,
            _subscribed,
            _blazeId,
        ];
    }

    public override Tdf CreateNew() => new UserSessionExtendedDataUpdate();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSessionExtendedDataUpdate";
    public override string GetFullClassName() => "Blaze::UserSessionExtendedDataUpdate";

    public Blaze15SDK.Blaze.UserSessionExtendedData? ExtendedData
    {
        get => _extendedData.Value;
        set => _extendedData.Value = value;
    }

    public bool Subscribed
    {
        get => _subscribed.Value;
        set => _subscribed.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
