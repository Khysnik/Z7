using Blaze.Core;
using Blaze15SDK.Blaze;
using Blaze15SDK.Blaze.Authentication;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class AuthenticationComponentBase
{
    public const ushort Id = 1;
    public const string Name = "AuthenticationComponent";

    public enum Error : ushort
    {
        AUTH_ERR_INVALID_TOKEN = 1,
        AUTH_ERR_INVALID_REQUEST = 2,
        AUTH_ERR_INVALID_AUTHCODE = 3,
        AUTH_ERR_INVALID_PLATFORM = 4,
        AUTH_ERR_INVALID_SANDBOX_ID = 6,
        AUTH_ERR_INVALID_COUNTRY = 10,
        AUTH_ERR_INVALID_USER = 11,
        AUTH_ERR_INVALID_PASSWORD = 12,
        AUTH_ERR_EXPIRED_TOKEN = 14,
        AUTH_ERR_EXISTS = 15,
        AUTH_ERR_TOO_YOUNG = 16,
        AUTH_ERR_NO_ACCOUNT = 17,
        AUTH_ERR_PERSONA_NOT_FOUND = 18,
        AUTH_ERR_PERSONA_INACTIVE = 19,
        AUTH_ERR_INVALID_PMAIL = 20,
        AUTH_ERR_INVALID_FIELD = 21,
        AUTH_ERR_INVALID_EMAIL = 22,
        AUTH_ERR_INVALID_STATUS = 23,
        AUTH_ERR_PERSONA_BANNED = 32,
        AUTH_ERR_INVALID_PERSONA = 33,
        AUTH_ERR_CURRENT_PASSWORD_REQUIRED = 34,
        AUTH_ERR_DEACTIVATED = 41,
        AUTH_ERR_BANNED = 43,
        AUTH_ERR_DISABLED = 44,
        AUTH_ERR_DUPLICATE_LOGIN = 45,
        AUTH_ERR_RESTRICTION_VIOLATION = 46,
        AUTH_ERR_MULTIPLE_WALLET_ACCOUNTS_FOUND = 47,
        AUTH_ERR_INVALID_PRODUCT_CONFIGURATION = 48,
        AUTH_ERR_ENTITLEMENT_TAG_MISSING = 49,
        AUTH_ERR_BAD_GATEWAY = 50,
        AUTH_ERR_NO_ASSOCIATED_PRODUCT = 54,
        AUTH_ERR_INVALID_MAPPING_ERROR = 55,
        AUTH_ERR_NO_SUCH_GROUP_NAME = 56,
        AUTH_ERR_MISSING_PERSONAID = 57,
        AUTH_ERR_USER_DOES_NOT_MATCH_PERSONA = 58,
        AUTH_ERR_WHITELIST = 59,
        AUTH_ERR_LINK_PERSONA = 60,
        AUTH_ERR_NO_SUCH_GROUP = 61,
        AUTH_ERR_NO_SUCH_ENTITLEMENT = 63,
        AUTH_ERR_USECOUNT_ZERO = 66,
        AUTH_ERR_ENTITLEMETNTAG_EMPTY = 67,
        AUTH_ERR_GROUPNAME_REQUIRED = 70,
        AUTH_ERR_GROUPNAME_INVALID = 71,
        AUTH_ERR_TOO_MANY_ENTITLEMENTS = 72,
        AUTH_ERR_PAGESIZE_ZERO = 73,
        AUTH_ERR_ENTITLEMENT_TAG_REQUIRED = 74,
        AUTH_ERR_PAGENO_ZERO = 75,
        AUTH_ERR_MODIFIED_STATUS_INVALID = 76,
        AUTH_ERR_USECOUNT_INCREMENT = 77,
        AUTH_ERR_TERMINATION_INVALID = 78,
        AUTH_ERR_UNKNOWN_ENTITLEMENT = 79,
        AUTH_ERR_EXCEEDS_PSU_LIMIT = 80,
        AUTH_ERR_OPTIN_NAME_REQUIRED = 81,
        AUTH_ERR_INVALID_OPTIN = 82,
        AUTH_ERR_OPTIN_MISMATCH = 83,
        AUTH_ERR_NO_SUCH_OPTIN = 84,
        AUTH_ERR_AUTHID_REQUIRED = 85,
        AUTH_ERR_PERSONA_EXTREFID_REQUIRED = 86,
        AUTH_ERR_SOURCE_REQUIRED = 87,
        AUTH_ERR_APPLICATION_REQUIRED = 88,
        AUTH_ERR_TOKEN_REQUIRED = 89,
        AUTH_ERR_PARAMETER_TOO_LENGTH = 90,
        AUTH_ERR_NO_SUCH_PERSONA_REFERENCE = 91,
        AUTH_ERR_INVALID_SOURCE = 93,
        AUTH_ERR_NO_SUCH_AUTH_DATA = 94,
        AUTH_ERR_USER_INACTIVE = 101,
        AUTH_ERR_UNEXPECTED_ACTIVATION = 102,
        AUTH_ERR_NAME_MISMATCH = 103,
        AUTH_ERR_INVALID_NAMESPACE = 105,
        AUTH_ERR_FIELD_MIN_LOWER_CHARS = 198,
        AUTH_ERR_FIELD_MIN_UPPER_CHARS = 199,
        AUTH_ERR_FIELD_MIN_DIGITS = 200,
        AUTH_ERR_FIELD_INVALID_CHARS = 201,
        AUTH_ERR_FIELD_TOO_SHORT = 202,
        AUTH_ERR_FIELD_MUST_BEGIN_WITH_LETTER = 204,
        AUTH_ERR_FIELD_MISSING = 205,
        AUTH_ERR_FIELD_INVALID = 206,
        AUTH_ERR_FIELD_NOT_ALLOWED = 207,
        AUTH_ERR_FIELD_NEEDS_SPECIAL_CHARS = 208,
        AUTH_ERR_FIELD_ALREADY_EXISTS = 209,
        AUTH_ERR_FIELD_NEEDS_CONSENT = 210,
        AUTH_ERR_FIELD_TOO_YOUNG = 211,
        AUTH_ERR_ASSOCIATION_TOO_YOUNG = 212,
        AUTH_ERR_TOO_MANY_PERSONA_FOR_NAMESPACE = 300,
        AUTH_ERR_NEEDS_PMAIL = 303,
        AUTH_ERR_NO_PARENT_SESSION = 307,
        AUTH_ERR_NO_XBLTOKEN = 308,
        AUTH_ERR_NO_PSNTOKEN = 309,
        AUTH_ERR_TRIAL_PERIOD_CLOSED = 310,
        AUTH_ERR_EXPIRED_1PTOKEN = 311,
    }

    public enum AuthenticationComponentCommand : ushort
    {
        login = 10,
        trustedLogin = 11,
        updateAccount = 20,
        listUserEntitlements2 = 29,
        getAccount = 30,
        grantEntitlement = 31,
        listEntitlements = 32,
        getUseCount = 34,
        decrementUseCount = 35,
        grantEntitlement2 = 39,
        modifyEntitlement2 = 43,
        getPrivacyPolicyContent = 47,
        listPersonaEntitlements2 = 48,
        getOptIn = 52,
        enableOptIn = 53,
        disableOptIn = 54,
        expressLogin = 60,
        stressLogin = 61,
        logout = 70,
        getPersona = 90,
        listPersonas = 100,
        createWalUserSession = 230,
        checkLegalDoc = 240,
        getEmailOptInSettings = 242,
        getTermsOfServiceContent = 246,
        getOriginPersona = 260,
        guestLogin = 290,
        getDecryptedBlazeIds = 300,
        updateAccessToken = 310,
    }

    public enum AuthenticationComponentNotification : ushort
    {
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => AuthenticationComponentBase.Id;
        public override string Name => AuthenticationComponentBase.Name;

        public virtual bool IsCommandSupported(AuthenticationComponentCommand command) => false;

        public class AuthenticationException : BlazeRpcException
        {
            public AuthenticationException(Error error) : base((ushort)error, null) { }
            public AuthenticationException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public AuthenticationException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public AuthenticationException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public AuthenticationException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public AuthenticationException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public AuthenticationException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public AuthenticationException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            RegisterCommand(new RpcCommandFunc<LoginRequest, LoginResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.login,
                Name = "login",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.login),
                Func = async (req, ctx) => await LoginAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<TrustedLoginRequest, LoginResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.trustedLogin,
                Name = "trustedLogin",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.trustedLogin),
                Func = async (req, ctx) => await TrustedLoginAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAccountRequest, UpdateAccountResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.updateAccount,
                Name = "updateAccount",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.updateAccount),
                Func = async (req, ctx) => await UpdateAccountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListUserEntitlements2Request, Entitlements, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.listUserEntitlements2,
                Name = "listUserEntitlements2",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.listUserEntitlements2),
                Func = async (req, ctx) => await ListUserEntitlements2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, AccountInfo, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getAccount,
                Name = "getAccount",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getAccount),
                Func = async (req, ctx) => await GetAccountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PostEntitlementRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.grantEntitlement,
                Name = "grantEntitlement",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.grantEntitlement),
                Func = async (req, ctx) => await GrantEntitlementAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListEntitlementsRequest, Entitlements, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.listEntitlements,
                Name = "listEntitlements",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.listEntitlements),
                Func = async (req, ctx) => await ListEntitlementsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetUseCountRequest, UseCount, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getUseCount,
                Name = "getUseCount",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getUseCount),
                Func = async (req, ctx) => await GetUseCountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DecrementUseCountRequest, DecrementUseCount, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.decrementUseCount,
                Name = "decrementUseCount",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.decrementUseCount),
                Func = async (req, ctx) => await DecrementUseCountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GrantEntitlement2Request, GrantEntitlement2Response, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.grantEntitlement2,
                Name = "grantEntitlement2",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.grantEntitlement2),
                Func = async (req, ctx) => await GrantEntitlement2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ModifyEntitlement2Request, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.modifyEntitlement2,
                Name = "modifyEntitlement2",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.modifyEntitlement2),
                Func = async (req, ctx) => await ModifyEntitlement2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetLegalDocContentRequest, GetLegalDocContentResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getPrivacyPolicyContent,
                Name = "getPrivacyPolicyContent",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getPrivacyPolicyContent),
                Func = async (req, ctx) => await GetPrivacyPolicyContentAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListPersonaEntitlements2Request, Entitlements, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.listPersonaEntitlements2,
                Name = "listPersonaEntitlements2",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.listPersonaEntitlements2),
                Func = async (req, ctx) => await ListPersonaEntitlements2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.Authentication.OptInRequest, OptInValue, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getOptIn,
                Name = "getOptIn",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getOptIn),
                Func = async (req, ctx) => await GetOptInAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.Authentication.OptInRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.enableOptIn,
                Name = "enableOptIn",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.enableOptIn),
                Func = async (req, ctx) => await EnableOptInAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.Authentication.OptInRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.disableOptIn,
                Name = "disableOptIn",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.disableOptIn),
                Func = async (req, ctx) => await DisableOptInAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ExpressLoginRequest, LoginResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.expressLogin,
                Name = "expressLogin",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.expressLogin),
                Func = async (req, ctx) => await ExpressLoginAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<StressLoginRequest, LoginResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.stressLogin,
                Name = "stressLogin",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.stressLogin),
                Func = async (req, ctx) => await StressLoginAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.logout,
                Name = "logout",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.logout),
                Func = async (req, ctx) => await LogoutAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetPersonaResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getPersona,
                Name = "getPersona",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getPersona),
                Func = async (req, ctx) => await GetPersonaAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, ListPersonasResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.listPersonas,
                Name = "listPersonas",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.listPersonas),
                Func = async (req, ctx) => await ListPersonasAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, UserLoginInfo, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.createWalUserSession,
                Name = "createWalUserSession",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.createWalUserSession),
                Func = async (req, ctx) => await CreateWalUserSessionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CheckLegalDocRequest, CheckLegalDocResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.checkLegalDoc,
                Name = "checkLegalDoc",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.checkLegalDoc),
                Func = async (req, ctx) => await CheckLegalDocAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetEmailOptInSettingsRequest, GetEmailOptInSettingsResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getEmailOptInSettings,
                Name = "getEmailOptInSettings",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getEmailOptInSettings),
                Func = async (req, ctx) => await GetEmailOptInSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetLegalDocContentRequest, GetLegalDocContentResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getTermsOfServiceContent,
                Name = "getTermsOfServiceContent",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getTermsOfServiceContent),
                Func = async (req, ctx) => await GetTermsOfServiceContentAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetOriginPersonaRequest, GetPersonaResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.getOriginPersona,
                Name = "getOriginPersona",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getOriginPersona),
                Func = async (req, ctx) => await GetOriginPersonaAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, SessionInfo, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.guestLogin,
                Name = "guestLogin",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.guestLogin),
                Func = async (req, ctx) => await GuestLoginAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetDecryptedBlazeIdsRequest, GetDecryptedBlazeIdsResponse, GetDecryptedBlazeIdsResponseError>()
            {
                Id = (ushort)AuthenticationComponentCommand.getDecryptedBlazeIds,
                Name = "getDecryptedBlazeIds",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.getDecryptedBlazeIds),
                Func = async (req, ctx) => await GetDecryptedBlazeIdsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAccessTokenRequest, UpdateAccessTokenResponse, EmptyMessage>()
            {
                Id = (ushort)AuthenticationComponentCommand.updateAccessToken,
                Name = "updateAccessToken",
                IsSupported = IsCommandSupported(AuthenticationComponentCommand.updateAccessToken),
                Func = async (req, ctx) => await UpdateAccessTokenAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::login</b> command.<br/>
        /// Request type: <see cref="LoginRequest"/><br/>
        /// Response type: <see cref="LoginResponse"/><br/>
        /// </summary>
        public virtual Task<LoginResponse> LoginAsync(LoginRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::trustedLogin</b> command.<br/>
        /// Request type: <see cref="TrustedLoginRequest"/><br/>
        /// Response type: <see cref="LoginResponse"/><br/>
        /// </summary>
        public virtual Task<LoginResponse> TrustedLoginAsync(TrustedLoginRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::updateAccount</b> command.<br/>
        /// Request type: <see cref="UpdateAccountRequest"/><br/>
        /// Response type: <see cref="UpdateAccountResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::listUserEntitlements2</b> command.<br/>
        /// Request type: <see cref="ListUserEntitlements2Request"/><br/>
        /// Response type: <see cref="Entitlements"/><br/>
        /// </summary>
        public virtual Task<Entitlements> ListUserEntitlements2Async(ListUserEntitlements2Request request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getAccount</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="AccountInfo"/><br/>
        /// </summary>
        public virtual Task<AccountInfo> GetAccountAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::grantEntitlement</b> command.<br/>
        /// Request type: <see cref="PostEntitlementRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GrantEntitlementAsync(PostEntitlementRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::listEntitlements</b> command.<br/>
        /// Request type: <see cref="ListEntitlementsRequest"/><br/>
        /// Response type: <see cref="Entitlements"/><br/>
        /// </summary>
        public virtual Task<Entitlements> ListEntitlementsAsync(ListEntitlementsRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getUseCount</b> command.<br/>
        /// Request type: <see cref="GetUseCountRequest"/><br/>
        /// Response type: <see cref="UseCount"/><br/>
        /// </summary>
        public virtual Task<UseCount> GetUseCountAsync(GetUseCountRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::decrementUseCount</b> command.<br/>
        /// Request type: <see cref="DecrementUseCountRequest"/><br/>
        /// Response type: <see cref="DecrementUseCount"/><br/>
        /// </summary>
        public virtual Task<DecrementUseCount> DecrementUseCountAsync(DecrementUseCountRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::grantEntitlement2</b> command.<br/>
        /// Request type: <see cref="GrantEntitlement2Request"/><br/>
        /// Response type: <see cref="GrantEntitlement2Response"/><br/>
        /// </summary>
        public virtual Task<GrantEntitlement2Response> GrantEntitlement2Async(GrantEntitlement2Request request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::modifyEntitlement2</b> command.<br/>
        /// Request type: <see cref="ModifyEntitlement2Request"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ModifyEntitlement2Async(ModifyEntitlement2Request request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getPrivacyPolicyContent</b> command.<br/>
        /// Request type: <see cref="GetLegalDocContentRequest"/><br/>
        /// Response type: <see cref="GetLegalDocContentResponse"/><br/>
        /// </summary>
        public virtual Task<GetLegalDocContentResponse> GetPrivacyPolicyContentAsync(GetLegalDocContentRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::listPersonaEntitlements2</b> command.<br/>
        /// Request type: <see cref="ListPersonaEntitlements2Request"/><br/>
        /// Response type: <see cref="Entitlements"/><br/>
        /// </summary>
        public virtual Task<Entitlements> ListPersonaEntitlements2Async(ListPersonaEntitlements2Request request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getOptIn</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.Authentication.OptInRequest"/><br/>
        /// Response type: <see cref="OptInValue"/><br/>
        /// </summary>
        public virtual Task<OptInValue> GetOptInAsync(Blaze15SDK.Blaze.Authentication.OptInRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::enableOptIn</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.Authentication.OptInRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> EnableOptInAsync(Blaze15SDK.Blaze.Authentication.OptInRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::disableOptIn</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.Authentication.OptInRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DisableOptInAsync(Blaze15SDK.Blaze.Authentication.OptInRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::expressLogin</b> command.<br/>
        /// Request type: <see cref="ExpressLoginRequest"/><br/>
        /// Response type: <see cref="LoginResponse"/><br/>
        /// </summary>
        public virtual Task<LoginResponse> ExpressLoginAsync(ExpressLoginRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::stressLogin</b> command.<br/>
        /// Request type: <see cref="StressLoginRequest"/><br/>
        /// Response type: <see cref="LoginResponse"/><br/>
        /// </summary>
        public virtual Task<LoginResponse> StressLoginAsync(StressLoginRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::logout</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> LogoutAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getPersona</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="GetPersonaResponse"/><br/>
        /// </summary>
        public virtual Task<GetPersonaResponse> GetPersonaAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::listPersonas</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="ListPersonasResponse"/><br/>
        /// </summary>
        public virtual Task<ListPersonasResponse> ListPersonasAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::createWalUserSession</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="UserLoginInfo"/><br/>
        /// </summary>
        public virtual Task<UserLoginInfo> CreateWalUserSessionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::checkLegalDoc</b> command.<br/>
        /// Request type: <see cref="CheckLegalDocRequest"/><br/>
        /// Response type: <see cref="CheckLegalDocResponse"/><br/>
        /// </summary>
        public virtual Task<CheckLegalDocResponse> CheckLegalDocAsync(CheckLegalDocRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getEmailOptInSettings</b> command.<br/>
        /// Request type: <see cref="GetEmailOptInSettingsRequest"/><br/>
        /// Response type: <see cref="GetEmailOptInSettingsResponse"/><br/>
        /// </summary>
        public virtual Task<GetEmailOptInSettingsResponse> GetEmailOptInSettingsAsync(GetEmailOptInSettingsRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getTermsOfServiceContent</b> command.<br/>
        /// Request type: <see cref="GetLegalDocContentRequest"/><br/>
        /// Response type: <see cref="GetLegalDocContentResponse"/><br/>
        /// </summary>
        public virtual Task<GetLegalDocContentResponse> GetTermsOfServiceContentAsync(GetLegalDocContentRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getOriginPersona</b> command.<br/>
        /// Request type: <see cref="GetOriginPersonaRequest"/><br/>
        /// Response type: <see cref="GetPersonaResponse"/><br/>
        /// </summary>
        public virtual Task<GetPersonaResponse> GetOriginPersonaAsync(GetOriginPersonaRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::guestLogin</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="SessionInfo"/><br/>
        /// </summary>
        public virtual Task<SessionInfo> GuestLoginAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::getDecryptedBlazeIds</b> command.<br/>
        /// Request type: <see cref="GetDecryptedBlazeIdsRequest"/><br/>
        /// Response type: <see cref="GetDecryptedBlazeIdsResponse"/><br/>
        /// Error response type: <see cref="GetDecryptedBlazeIdsResponseError"/><br/>
        /// </summary>
        public virtual Task<GetDecryptedBlazeIdsResponse> GetDecryptedBlazeIdsAsync(GetDecryptedBlazeIdsRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AuthenticationComponent::updateAccessToken</b> command.<br/>
        /// Request type: <see cref="UpdateAccessTokenRequest"/><br/>
        /// Response type: <see cref="UpdateAccessTokenResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateAccessTokenResponse> UpdateAccessTokenAsync(UpdateAccessTokenRequest request, BlazeRpcContext context)
        {
            throw new AuthenticationException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
