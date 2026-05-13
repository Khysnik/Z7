using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.CensusData;

public class SubscribeToCensusDataUpdatesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CensusNotificationPeriod", "mCensusNotificationPeriod", 0x8EEC0000, TdfType.Int64, 0, true), // Tag: CNP
        new TdfMemberInfo("NotificationTimeout", "mNotificationTimeout", 0xBB4B7400, TdfType.Int64, 1, true), // Tag: NTMT
        new TdfMemberInfo("ResubscribeTimeout", "mResubscribeTimeout", 0xCB4B7400, TdfType.Int64, 2, true), // Tag: RTMT
    ];
    private ITdfMember[] __members;

    private TdfInt64 _censusNotificationPeriod = new(__typeInfos[0]);
    private TdfInt64 _notificationTimeout = new(__typeInfos[1]);
    private TdfInt64 _resubscribeTimeout = new(__typeInfos[2]);

    public SubscribeToCensusDataUpdatesResponse()
    {
        __members = [ _censusNotificationPeriod, _notificationTimeout, _resubscribeTimeout ];
    }

    public override Tdf CreateNew() => new SubscribeToCensusDataUpdatesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SubscribeToCensusDataUpdatesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::SubscribeToCensusDataUpdatesResponse";

    public long CensusNotificationPeriod
    {
        get => _censusNotificationPeriod.Value;
        set => _censusNotificationPeriod.Value = value;
    }

    public long NotificationTimeout
    {
        get => _notificationTimeout.Value;
        set => _notificationTimeout.Value = value;
    }

    public long ResubscribeTimeout
    {
        get => _resubscribeTimeout.Value;
        set => _resubscribeTimeout.Value = value;
    }

}
