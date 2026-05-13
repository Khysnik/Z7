using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class ContextHistory : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F296100, TdfType.Int64, 0, true), // Tag: CREA
        new TdfMemberInfo("Deleted", "mDeleted", 0x925B0000, TdfType.Bool, 1, true), // Tag: DEL
        new TdfMemberInfo("Description", "mDescription", 0x925CE300, TdfType.String, 2, true), // Tag: DESC
        new TdfMemberInfo("Label", "mLabel", 0xB218AC00, TdfType.String, 3, true), // Tag: LABL
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 4, true), // Tag: NAME
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xD7093400, TdfType.Int64, 5, true), // Tag: UPDT
        new TdfMemberInfo("Version", "mVersion", 0xDA5CB300, TdfType.UInt32, 6, true), // Tag: VERS
    ];
    private ITdfMember[] __members;

    private TdfInt64 _creationTime = new(__typeInfos[0]);
    private TdfBool _deleted = new(__typeInfos[1]);
    private TdfString _description = new(__typeInfos[2]);
    private TdfString _label = new(__typeInfos[3]);
    private TdfString _name = new(__typeInfos[4]);
    private TdfInt64 _lastUpdateTime = new(__typeInfos[5]);
    private TdfUInt32 _version = new(__typeInfos[6]);

    public ContextHistory()
    {
        __members = [ _creationTime, _deleted, _description, _label, _name, _lastUpdateTime, _version ];
    }

    public override Tdf CreateNew() => new ContextHistory();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ContextHistory";
    public override string GetFullClassName() => "Blaze::ByteVault::ContextHistory";

    public long CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public bool Deleted
    {
        get => _deleted.Value;
        set => _deleted.Value = value;
    }

    public string Description
    {
        get => _description.Value;
        set => _description.Value = value;
    }

    public string Label
    {
        get => _label.Value;
        set => _label.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public long LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

    public uint Version
    {
        get => _version.Value;
        set => _version.Value = value;
    }

}
