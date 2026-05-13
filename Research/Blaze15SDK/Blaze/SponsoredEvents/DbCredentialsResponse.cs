using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class DbCredentialsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DbHost", "mDbHost", 0xA2FCF400, TdfType.String, 0, true), // Tag: HOST
        new TdfMemberInfo("DbInstanceName", "mDbInstanceName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
        new TdfMemberInfo("DbPort", "mDbPort", 0xC2FCB400, TdfType.UInt32, 2, true), // Tag: PORT
        new TdfMemberInfo("DbSchema", "mDbSchema", 0xCE3A2D00, TdfType.String, 3, true), // Tag: SCHM
    ];
    private ITdfMember[] __members;

    private TdfString _dbHost = new(__typeInfos[0]);
    private TdfString _dbInstanceName = new(__typeInfos[1]);
    private TdfUInt32 _dbPort = new(__typeInfos[2]);
    private TdfString _dbSchema = new(__typeInfos[3]);

    public DbCredentialsResponse()
    {
        __members = [_dbHost, _dbInstanceName, _dbPort, _dbSchema];
    }

    public override Tdf CreateNew() => new DbCredentialsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DbCredentialsResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::DbCredentialsResponse";

    public string DbHost
    {
        get => _dbHost.Value;
        set => _dbHost.Value = value;
    }

    public string DbInstanceName
    {
        get => _dbInstanceName.Value;
        set => _dbInstanceName.Value = value;
    }

    public uint DbPort
    {
        get => _dbPort.Value;
        set => _dbPort.Value = value;
    }

    public string DbSchema
    {
        get => _dbSchema.Value;
        set => _dbSchema.Value = value;
    }
}
