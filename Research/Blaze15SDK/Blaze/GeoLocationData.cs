using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class GeoLocationData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Country", "mCountry", 0x8EED3900, TdfType.String, 0, true), // Tag: CNTY
        new TdfMemberInfo("City", "mCity", 0x8F4E4000, TdfType.String, 1, true), // Tag: CTY
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xA6400000, TdfType.Int64, 2, true), // Tag: ID
        new TdfMemberInfo("ISP", "mISP", 0xA73C0000, TdfType.String, 3, true), // Tag: ISP
        new TdfMemberInfo("Latitude", "mLatitude", 0xB21D0000, TdfType.Float, 4, true), // Tag: LAT
        new TdfMemberInfo("Longitude", "mLongitude", 0xB2FB8000, TdfType.Float, 5, true), // Tag: LON
        new TdfMemberInfo("OptIn", "mOptIn", 0xBF0D0000, TdfType.Bool, 6, true), // Tag: OPT
        new TdfMemberInfo("IsOverridden", "mIsOverridden", 0xBF697200, TdfType.Bool, 7, true), // Tag: OVER
        new TdfMemberInfo("StateRegion", "mStateRegion", 0xCF400000, TdfType.String, 8, true), // Tag: ST
        new TdfMemberInfo("TimeZone", "mTimeZone", 0xD3A00000, TdfType.String, 9, true), // Tag: TZ
    ];
    private ITdfMember[] __members;

    private TdfString _country = new(__typeInfos[0]);
    private TdfString _city = new(__typeInfos[1]);
    private TdfInt64 _blazeId = new(__typeInfos[2]);
    private TdfString _isp = new(__typeInfos[3]);
    private TdfFloat _latitude = new(__typeInfos[4]);
    private TdfFloat _longitude = new(__typeInfos[5]);
    private TdfBool _optIn = new(__typeInfos[6]);
    private TdfBool _isOverridden = new(__typeInfos[7]);
    private TdfString _stateRegion = new(__typeInfos[8]);
    private TdfString _timeZone = new(__typeInfos[9]);

    public GeoLocationData()
    {
        __members = [
            _country,
            _city,
            _blazeId,
            _isp,
            _latitude,
            _longitude,
            _optIn,
            _isOverridden,
            _stateRegion,
            _timeZone,
        ];
    }

    public override Tdf CreateNew() => new GeoLocationData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GeoLocationData";
    public override string GetFullClassName() => "Blaze::GeoLocationData";

    public string Country
    {
        get => _country.Value;
        set => _country.Value = value;
    }

    public string City
    {
        get => _city.Value;
        set => _city.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public string ISP
    {
        get => _isp.Value;
        set => _isp.Value = value;
    }

    public float Latitude
    {
        get => _latitude.Value;
        set => _latitude.Value = value;
    }

    public float Longitude
    {
        get => _longitude.Value;
        set => _longitude.Value = value;
    }

    public bool OptIn
    {
        get => _optIn.Value;
        set => _optIn.Value = value;
    }

    public bool IsOverridden
    {
        get => _isOverridden.Value;
        set => _isOverridden.Value = value;
    }

    public string StateRegion
    {
        get => _stateRegion.Value;
        set => _stateRegion.Value = value;
    }

    public string TimeZone
    {
        get => _timeZone.Value;
        set => _timeZone.Value = value;
    }

}
