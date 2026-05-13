using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class AccountInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AnonymousUser", "mAnonymousUser", 0x86DD4000, TdfType.Bool, 0, true), // Tag: AMU
        new TdfMemberInfo("AuthenticationSource", "mAuthenticationSource", 0x873CA300, TdfType.String, 1, true), // Tag: ASRC
        new TdfMemberInfo("Country", "mCountry", 0x8EF00000, TdfType.String, 2, true), // Tag: CO
        new TdfMemberInfo("DOB", "mDOB", 0x92F88000, TdfType.String, 3, true), // Tag: DOB
        new TdfMemberInfo("DateCreated", "mDateCreated", 0x9348F200, TdfType.String, 4, true), // Tag: DTCR
        new TdfMemberInfo("GlobalOptin", "mGlobalOptin", 0x9EFC3400, TdfType.Int8, 5, true), // Tag: GOPT
        new TdfMemberInfo("LastAuth", "mLastAuth", 0xB21D2800, TdfType.String, 6, true), // Tag: LATH
        new TdfMemberInfo("Language", "mLanguage", 0xB2E00000, TdfType.String, 7, true), // Tag: LN
        new TdfMemberInfo("Locale", "mLocale", 0xB2F8C000, TdfType.String, 8, true), // Tag: LOC
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 9, true), // Tag: MAIL
        new TdfMemberInfo("ParentalEmail", "mParentalEmail", 0xC2DB0000, TdfType.String, 10, true), // Tag: PML
        new TdfMemberInfo("ReasonCode", "mReasonCode", 0xCA300000, TdfType.Enum, 11, true), // Tag: RC
        new TdfMemberInfo("Status", "mStatus", 0xCF487300, TdfType.Enum, 12, true), // Tag: STAS
        new TdfMemberInfo("EmailStatus", "mEmailStatus", 0xCF487400, TdfType.Enum, 13, true), // Tag: STAT
        new TdfMemberInfo("ThirdPartyOptin", "mThirdPartyOptin", 0xD30BF400, TdfType.Int8, 14, true), // Tag: TPOT
        new TdfMemberInfo("UnderageUser", "mUnderageUser", 0xD64D4000, TdfType.Bool, 15, true), // Tag: UDU
        new TdfMemberInfo("AccountId", "mAccountId", 0xD6990000, TdfType.Int64, 16, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfBool _anonymousUser = new(__typeInfos[0]);
    private TdfString _authenticationSource = new(__typeInfos[1]);
    private TdfString _country = new(__typeInfos[2]);
    private TdfString _dob = new(__typeInfos[3]);
    private TdfString _dateCreated = new(__typeInfos[4]);
    private TdfInt8 _globalOptin = new(__typeInfos[5]);
    private TdfString _lastAuth = new(__typeInfos[6]);
    private TdfString _language = new(__typeInfos[7]);
    private TdfString _locale = new(__typeInfos[8]);
    private TdfString _email = new(__typeInfos[9]);
    private TdfString _parentalEmail = new(__typeInfos[10]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.StatusReason> _reasonCode = new(__typeInfos[11]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.AccountStatus> _status = new(__typeInfos[12]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EmailStatus> _emailStatus = new(__typeInfos[13]);
    private TdfInt8 _thirdPartyOptin = new(__typeInfos[14]);
    private TdfBool _underageUser = new(__typeInfos[15]);
    private TdfInt64 _accountId = new(__typeInfos[16]);

    public AccountInfo()
    {
        __members = [
            _anonymousUser,
            _authenticationSource,
            _country,
            _dob,
            _dateCreated,
            _globalOptin,
            _lastAuth,
            _language,
            _locale,
            _email,
            _parentalEmail,
            _reasonCode,
            _status,
            _emailStatus,
            _thirdPartyOptin,
            _underageUser,
            _accountId,
        ];
    }

    public override Tdf CreateNew() => new AccountInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AccountInfo";
    public override string GetFullClassName() => "Blaze::Authentication::AccountInfo";

    public bool AnonymousUser { get => _anonymousUser.Value; set => _anonymousUser.Value = value; }
    public string AuthenticationSource { get => _authenticationSource.Value; set => _authenticationSource.Value = value; }
    public string Country { get => _country.Value; set => _country.Value = value; }
    public string DOB { get => _dob.Value; set => _dob.Value = value; }
    public string DateCreated { get => _dateCreated.Value; set => _dateCreated.Value = value; }
    public sbyte GlobalOptin { get => _globalOptin.Value; set => _globalOptin.Value = value; }
    public string LastAuth { get => _lastAuth.Value; set => _lastAuth.Value = value; }
    public string Language { get => _language.Value; set => _language.Value = value; }
    public string Locale { get => _locale.Value; set => _locale.Value = value; }
    public string Email { get => _email.Value; set => _email.Value = value; }
    public string ParentalEmail { get => _parentalEmail.Value; set => _parentalEmail.Value = value; }
    public Blaze15SDK.Blaze.Authentication.StatusReason ReasonCode { get => _reasonCode.Value; set => _reasonCode.Value = value; }
    public Blaze15SDK.Blaze.Authentication.AccountStatus Status { get => _status.Value; set => _status.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EmailStatus EmailStatus { get => _emailStatus.Value; set => _emailStatus.Value = value; }
    public sbyte ThirdPartyOptin { get => _thirdPartyOptin.Value; set => _thirdPartyOptin.Value = value; }
    public bool UnderageUser { get => _underageUser.Value; set => _underageUser.Value = value; }
    public long AccountId { get => _accountId.Value; set => _accountId.Value = value; }
}
