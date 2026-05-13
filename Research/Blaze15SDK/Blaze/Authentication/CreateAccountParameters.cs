using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class CreateAccountParameters : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BirthDay", "mBirthDay", 0x8A487900, TdfType.Int32, 0, true), // Tag: BDAY
        new TdfMemberInfo("BirthMonth", "mBirthMonth", 0x8ADBEE00, TdfType.Int32, 1, true), // Tag: BMON
        new TdfMemberInfo("BirthYear", "mBirthYear", 0x8B9C8000, TdfType.UInt32, 2, true), // Tag: BYR
        new TdfMemberInfo("IsoCountryCode", "mIsoCountryCode", 0x8F4CB900, TdfType.String, 3, true), // Tag: CTRY
        new TdfMemberInfo("IsoLanguageCode", "mIsoLanguageCode", 0xB21BA700, TdfType.String, 4, true), // Tag: LANG
        new TdfMemberInfo("IsoLocaleCode", "mIsoLocaleCode", 0xB2F8EC00, TdfType.String, 5, true), // Tag: LOCL
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 6, true), // Tag: MAIL
        new TdfMemberInfo("EaEmailAllowed", "mEaEmailAllowed", 0xBF0D1100, TdfType.UInt8, 7, true), // Tag: OPT1
        new TdfMemberInfo("ThirdPartyEmailAllowed", "mThirdPartyEmailAllowed", 0xBF0D1300, TdfType.UInt8, 8, true), // Tag: OPT3
        new TdfMemberInfo("Password", "mPassword", 0xC21CF300, TdfType.String, 9, true), // Tag: PASS
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC2E86D00, TdfType.String, 10, true), // Tag: PNAM
        new TdfMemberInfo("ProductName", "mProductName", 0xC3293400, TdfType.String, 11, true), // Tag: PRDT
        new TdfMemberInfo("PrivacyPolicyUri", "mPrivacyPolicyUri", 0xC32A7600, TdfType.String, 12, true), // Tag: PRIV
        new TdfMemberInfo("ParentalEmail", "mParentalEmail", 0xC32BB400, TdfType.String, 13, true), // Tag: PRNT
        new TdfMemberInfo("UserProfileInfo", "mUserProfileInfo", 0xC32BE600, TdfType.Struct, 14, true), // Tag: PROF
        new TdfMemberInfo("TermsOfServiceUri", "mTermsOfServiceUri", 0xD33D6900, TdfType.String, 15, true), // Tag: TSUI
    ];
    private ITdfMember[] __members;

    private TdfInt32 _birthDay = new(__typeInfos[0]);
    private TdfInt32 _birthMonth = new(__typeInfos[1]);
    private TdfUInt32 _birthYear = new(__typeInfos[2]);
    private TdfString _isoCountryCode = new(__typeInfos[3]);
    private TdfString _isoLanguageCode = new(__typeInfos[4]);
    private TdfString _isoLocaleCode = new(__typeInfos[5]);
    private TdfString _email = new(__typeInfos[6]);
    private TdfUInt8 _eaEmailAllowed = new(__typeInfos[7]);
    private TdfUInt8 _thirdPartyEmailAllowed = new(__typeInfos[8]);
    private TdfString _password = new(__typeInfos[9]);
    private TdfString _personaName = new(__typeInfos[10]);
    private TdfString _productName = new(__typeInfos[11]);
    private TdfString _privacyPolicyUri = new(__typeInfos[12]);
    private TdfString _parentalEmail = new(__typeInfos[13]);
    private TdfStruct<Blaze15SDK.Blaze.Authentication.UserProfileInfo?> _userProfileInfo = new(__typeInfos[14]);
    private TdfString _termsOfServiceUri = new(__typeInfos[15]);

    public CreateAccountParameters()
    {
        __members = [
            _birthDay,
            _birthMonth,
            _birthYear,
            _isoCountryCode,
            _isoLanguageCode,
            _isoLocaleCode,
            _email,
            _eaEmailAllowed,
            _thirdPartyEmailAllowed,
            _password,
            _personaName,
            _productName,
            _privacyPolicyUri,
            _parentalEmail,
            _userProfileInfo,
            _termsOfServiceUri,
        ];
    }

    public override Tdf CreateNew() => new CreateAccountParameters();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreateAccountParameters";
    public override string GetFullClassName() => "Blaze::Authentication::CreateAccountParameters";

    public int BirthDay { get => _birthDay.Value; set => _birthDay.Value = value; }
    public int BirthMonth { get => _birthMonth.Value; set => _birthMonth.Value = value; }
    public uint BirthYear { get => _birthYear.Value; set => _birthYear.Value = value; }
    public string IsoCountryCode { get => _isoCountryCode.Value; set => _isoCountryCode.Value = value; }
    public string IsoLanguageCode { get => _isoLanguageCode.Value; set => _isoLanguageCode.Value = value; }
    public string IsoLocaleCode { get => _isoLocaleCode.Value; set => _isoLocaleCode.Value = value; }
    public string Email { get => _email.Value; set => _email.Value = value; }
    public byte EaEmailAllowed { get => _eaEmailAllowed.Value; set => _eaEmailAllowed.Value = value; }
    public byte ThirdPartyEmailAllowed { get => _thirdPartyEmailAllowed.Value; set => _thirdPartyEmailAllowed.Value = value; }
    public string Password { get => _password.Value; set => _password.Value = value; }
    public string PersonaName { get => _personaName.Value; set => _personaName.Value = value; }
    public string ProductName { get => _productName.Value; set => _productName.Value = value; }
    public string PrivacyPolicyUri { get => _privacyPolicyUri.Value; set => _privacyPolicyUri.Value = value; }
    public string ParentalEmail { get => _parentalEmail.Value; set => _parentalEmail.Value = value; }
    public Blaze15SDK.Blaze.Authentication.UserProfileInfo? UserProfileInfo { get => _userProfileInfo.Value; set => _userProfileInfo.Value = value; }
    public string TermsOfServiceUri { get => _termsOfServiceUri.Value; set => _termsOfServiceUri.Value = value; }
}
