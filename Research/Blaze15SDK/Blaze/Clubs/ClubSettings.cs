using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubSettings : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ArtPackageType", "mArtPackageType", 0x872C3400, TdfType.Enum, 0, true), // Tag: ARPT
        new TdfMemberInfo("BannerId", "mBannerId", 0x8AEA6400, TdfType.UInt32, 1, true), // Tag: BNID
        new TdfMemberInfo("ClubArtSettingsFlags", "mClubArtSettingsFlags", 0x8E1CE600, TdfType.Enum, 2, true), // Tag: CASF
        new TdfMemberInfo("AcceptanceFlags", "mAcceptanceFlags", 0x8EC86600, TdfType.Enum, 3, true), // Tag: CLAF
        new TdfMemberInfo("CustClubSettings", "mCustClubSettings", 0x8EC8F300, TdfType.Struct, 4, true), // Tag: CLCS
        new TdfMemberInfo("MetaData2", "mMetaData2", 0x8EC91200, TdfType.String, 5, true), // Tag: CLD2
        new TdfMemberInfo("Description", "mDescription", 0x8EC93300, TdfType.String, 6, true), // Tag: CLDS
        new TdfMemberInfo("MetaDataUnion", "mMetaDataUnion", 0x8EC93500, TdfType.Struct, 7, true), // Tag: CLDU
        new TdfMemberInfo("MetaData", "mMetaData", 0x8ECB6400, TdfType.String, 8, true), // Tag: CLMD
        new TdfMemberInfo("Region", "mRegion", 0x8ECCA700, TdfType.UInt32, 9, true), // Tag: CLRG
        new TdfMemberInfo("HasPassword", "mHasPassword", 0xA33C3700, TdfType.Bool, 10, true), // Tag: HSPW
        new TdfMemberInfo("IsNameProfane", "mIsNameProfane", 0xA73BB000, TdfType.Bool, 11, true), // Tag: ISNP
        new TdfMemberInfo("JoinAcceptance", "mJoinAcceptance", 0xAAFBA100, TdfType.Enum, 12, true), // Tag: JONA
        new TdfMemberInfo("Language", "mLanguage", 0xB21BA700, TdfType.String, 13, true), // Tag: LANG
        new TdfMemberInfo("LogoId", "mLogoId", 0xB2FA6400, TdfType.UInt32, 14, true), // Tag: LOID
        new TdfMemberInfo("LastUpdatedBy", "mLastUpdatedBy", 0xB358B900, TdfType.Int64, 15, true), // Tag: LUBY
        new TdfMemberInfo("LastSeasonLevelUpdate", "mLastSeasonLevelUpdate", 0xB35C2400, TdfType.Int32, 16, true), // Tag: LUPD
        new TdfMemberInfo("MetaData2Union", "mMetaData2Union", 0xB64D5200, TdfType.Struct, 17, true), // Tag: MDU2
        new TdfMemberInfo("ClubName", "mClubName", 0xBA1B6500, TdfType.String, 18, true), // Tag: NAME
        new TdfMemberInfo("NonUniqueName", "mNonUniqueName", 0xBB5C6E00, TdfType.String, 19, true), // Tag: NUQN
        new TdfMemberInfo("PreviousSeasonLevel", "mPreviousSeasonLevel", 0xC2CDAC00, TdfType.UInt32, 20, true), // Tag: PLVL
        new TdfMemberInfo("Password", "mPassword", 0xC33DE400, TdfType.String, 21, true), // Tag: PSWD
        new TdfMemberInfo("PetitionAcceptance", "mPetitionAcceptance", 0xC34BA100, TdfType.Enum, 22, true), // Tag: PTNA
        new TdfMemberInfo("SeasonLevel", "mSeasonLevel", 0xCECDAC00, TdfType.UInt32, 23, true), // Tag: SLVL
        new TdfMemberInfo("SkipPasswordCheckAcceptance", "mSkipPasswordCheckAcceptance", 0xCF08E100, TdfType.Enum, 24, true), // Tag: SPCA
        new TdfMemberInfo("TeamId", "mTeamId", 0xD2DA6400, TdfType.UInt32, 25, true), // Tag: TMID
    ];
    private ITdfMember[] __members;

    private TdfEnum<ArtPackageType> _artPackageType = new(__typeInfos[0]);
    private TdfUInt32 _bannerId = new(__typeInfos[1]);
    private TdfEnum<ClubArtSettingsFlags> _clubArtSettingsFlags = new(__typeInfos[2]);
    private TdfEnum<ClubAcceptanceFlags> _acceptanceFlags = new(__typeInfos[3]);
    private TdfStruct<CustClubSettings?> _custClubSettings = new(__typeInfos[4]);
    private TdfString _metaData2 = new(__typeInfos[5]);
    private TdfString _description = new(__typeInfos[6]);
    private TdfStruct<MetadataUnion?> _metaDataUnion = new(__typeInfos[7]);
    private TdfString _metaData = new(__typeInfos[8]);
    private TdfUInt32 _region = new(__typeInfos[9]);
    private TdfBool _hasPassword = new(__typeInfos[10]);
    private TdfBool _isNameProfane = new(__typeInfos[11]);
    private TdfEnum<RequestorAcceptance> _joinAcceptance = new(__typeInfos[12]);
    private TdfString _language = new(__typeInfos[13]);
    private TdfUInt32 _logoId = new(__typeInfos[14]);
    private TdfInt64 _lastUpdatedBy = new(__typeInfos[15]);
    private TdfInt32 _lastSeasonLevelUpdate = new(__typeInfos[16]);
    private TdfStruct<MetadataUnion?> _metaData2Union = new(__typeInfos[17]);
    private TdfString _clubName = new(__typeInfos[18]);
    private TdfString _nonUniqueName = new(__typeInfos[19]);
    private TdfUInt32 _previousSeasonLevel = new(__typeInfos[20]);
    private TdfString _password = new(__typeInfos[21]);
    private TdfEnum<RequestorAcceptance> _petitionAcceptance = new(__typeInfos[22]);
    private TdfUInt32 _seasonLevel = new(__typeInfos[23]);
    private TdfEnum<RequestorAcceptance> _skipPasswordCheckAcceptance = new(__typeInfos[24]);
    private TdfUInt32 _teamId = new(__typeInfos[25]);

    public ClubSettings()
    {
        __members = [ _artPackageType, _bannerId, _clubArtSettingsFlags, _acceptanceFlags, _custClubSettings, _metaData2, _description, _metaDataUnion, _metaData, _region, _hasPassword, _isNameProfane, _joinAcceptance, _language, _logoId, _lastUpdatedBy, _lastSeasonLevelUpdate, _metaData2Union, _clubName, _nonUniqueName, _previousSeasonLevel, _password, _petitionAcceptance, _seasonLevel, _skipPasswordCheckAcceptance, _teamId ];
    }

    public override Tdf CreateNew() => new ClubSettings();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubSettings";
    public override string GetFullClassName() => "Blaze::Clubs::ClubSettings";

    public ArtPackageType ArtPackageType
    {
        get => _artPackageType.Value;
        set => _artPackageType.Value = value;
    }

    public uint BannerId
    {
        get => _bannerId.Value;
        set => _bannerId.Value = value;
    }

    public ClubArtSettingsFlags ClubArtSettingsFlags
    {
        get => _clubArtSettingsFlags.Value;
        set => _clubArtSettingsFlags.Value = value;
    }

    public ClubAcceptanceFlags AcceptanceFlags
    {
        get => _acceptanceFlags.Value;
        set => _acceptanceFlags.Value = value;
    }

    public CustClubSettings? CustClubSettings
    {
        get => _custClubSettings.Value;
        set => _custClubSettings.Value = value;
    }

    public string MetaData2
    {
        get => _metaData2.Value;
        set => _metaData2.Value = value;
    }

    public string Description
    {
        get => _description.Value;
        set => _description.Value = value;
    }

    public MetadataUnion? MetaDataUnion
    {
        get => _metaDataUnion.Value;
        set => _metaDataUnion.Value = value;
    }

    public string MetaData
    {
        get => _metaData.Value;
        set => _metaData.Value = value;
    }

    public uint Region
    {
        get => _region.Value;
        set => _region.Value = value;
    }

    public bool HasPassword
    {
        get => _hasPassword.Value;
        set => _hasPassword.Value = value;
    }

    public bool IsNameProfane
    {
        get => _isNameProfane.Value;
        set => _isNameProfane.Value = value;
    }

    public RequestorAcceptance JoinAcceptance
    {
        get => _joinAcceptance.Value;
        set => _joinAcceptance.Value = value;
    }

    public string Language
    {
        get => _language.Value;
        set => _language.Value = value;
    }

    public uint LogoId
    {
        get => _logoId.Value;
        set => _logoId.Value = value;
    }

    public long LastUpdatedBy
    {
        get => _lastUpdatedBy.Value;
        set => _lastUpdatedBy.Value = value;
    }

    public int LastSeasonLevelUpdate
    {
        get => _lastSeasonLevelUpdate.Value;
        set => _lastSeasonLevelUpdate.Value = value;
    }

    public MetadataUnion? MetaData2Union
    {
        get => _metaData2Union.Value;
        set => _metaData2Union.Value = value;
    }

    public string ClubName
    {
        get => _clubName.Value;
        set => _clubName.Value = value;
    }

    public string NonUniqueName
    {
        get => _nonUniqueName.Value;
        set => _nonUniqueName.Value = value;
    }

    public uint PreviousSeasonLevel
    {
        get => _previousSeasonLevel.Value;
        set => _previousSeasonLevel.Value = value;
    }

    public string Password
    {
        get => _password.Value;
        set => _password.Value = value;
    }

    public RequestorAcceptance PetitionAcceptance
    {
        get => _petitionAcceptance.Value;
        set => _petitionAcceptance.Value = value;
    }

    public uint SeasonLevel
    {
        get => _seasonLevel.Value;
        set => _seasonLevel.Value = value;
    }

    public RequestorAcceptance SkipPasswordCheckAcceptance
    {
        get => _skipPasswordCheckAcceptance.Value;
        set => _skipPasswordCheckAcceptance.Value = value;
    }

    public uint TeamId
    {
        get => _teamId.Value;
        set => _teamId.Value = value;
    }

}
