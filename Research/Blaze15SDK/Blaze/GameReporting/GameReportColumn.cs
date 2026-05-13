using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GameReportColumn : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Key", "mKey", 0x8EB97900, TdfType.Struct, 0, true), // Tag: CKEY
        new TdfMemberInfo("UserCoreIdentName", "mUserCoreIdentName", 0x8EFCA500, TdfType.String, 1, true), // Tag: CORE
        new TdfMemberInfo("Type", "mType", 0x934E7000, TdfType.Int32, 2, true), // Tag: DTYP
        new TdfMemberInfo("EntityType", "mEntityType", 0x974E7000, TdfType.ObjectType, 3, true), // Tag: ETYP
        new TdfMemberInfo("Format", "mFormat", 0x9B2B7400, TdfType.String, 4, true), // Tag: FRMT
        new TdfMemberInfo("Kind", "mKind", 0xAE9BA400, TdfType.String, 5, true), // Tag: KIND
        new TdfMemberInfo("Desc", "mDesc", 0xB24CE300, TdfType.String, 6, true), // Tag: LDSC
        new TdfMemberInfo("Metadata", "mMetadata", 0xB65D2100, TdfType.String, 7, true), // Tag: META
        new TdfMemberInfo("ShortDesc", "mShortDesc", 0xCE4CE300, TdfType.String, 8, true), // Tag: SDSC
        new TdfMemberInfo("UnknownValue", "mUnknownValue", 0xD6EAF600, TdfType.String, 9, true), // Tag: UNKV
        new TdfMemberInfo("Values", "mValues", 0xDA1B3500, TdfType.List, 10, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfStruct<GameReportColumnKey?> _key = new(__typeInfos[0]);
    private TdfString _userCoreIdentName = new(__typeInfos[1]);
    private TdfInt32 _type = new(__typeInfos[2]);
    private TdfObjectType _entityType = new(__typeInfos[3]);
    private TdfString _format = new(__typeInfos[4]);
    private TdfString _kind = new(__typeInfos[5]);
    private TdfString _desc = new(__typeInfos[6]);
    private TdfString _metadata = new(__typeInfos[7]);
    private TdfString _shortDesc = new(__typeInfos[8]);
    private TdfString _unknownValue = new(__typeInfos[9]);
    private TdfList<string> _values = new(__typeInfos[10]);

    public GameReportColumn()
    {
        __members = [ _key, _userCoreIdentName, _type, _entityType, _format, _kind, _desc, _metadata, _shortDesc, _unknownValue, _values ];
    }

    public override Tdf CreateNew() => new GameReportColumn();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameReportColumn";
    public override string GetFullClassName() => "Blaze::GameReporting::GameReportColumn";

    public GameReportColumnKey? Key
    {
        get => _key.Value;
        set => _key.Value = value;
    }

    public string UserCoreIdentName
    {
        get => _userCoreIdentName.Value;
        set => _userCoreIdentName.Value = value;
    }

    public int Type
    {
        get => _type.Value;
        set => _type.Value = value;
    }

    public ObjectType EntityType
    {
        get => _entityType.Value;
        set => _entityType.Value = value;
    }

    public string Format
    {
        get => _format.Value;
        set => _format.Value = value;
    }

    public string Kind
    {
        get => _kind.Value;
        set => _kind.Value = value;
    }

    public string Desc
    {
        get => _desc.Value;
        set => _desc.Value = value;
    }

    public string Metadata
    {
        get => _metadata.Value;
        set => _metadata.Value = value;
    }

    public string ShortDesc
    {
        get => _shortDesc.Value;
        set => _shortDesc.Value = value;
    }

    public string UnknownValue
    {
        get => _unknownValue.Value;
        set => _unknownValue.Value = value;
    }

    public IList<string> Values
    {
        get => _values.Value;
        set => _values.Value = value;
    }

}
