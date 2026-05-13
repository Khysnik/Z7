using Blaze.Core;
using Blaze15SDK.Blaze.ByteVault;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class ByteVaultComponentBase
{
    public const ushort Id = 31;
    public const string Name = "ByteVaultComponent";

    public enum Error : ushort {
        BYTEVAULT_RECORD_REDIRECT = 1,
        BYTEVAULT_INVALID_CONTEXT = 2,
        BYTEVAULT_MISSING_CONTEXT = 3,
        BYTEVAULT_MISSING_LABEL = 4,
        BYTEVAULT_MISSING_DESCRIPTION = 5,
        BYTEVAULT_INVALID_CATEGORY = 6,
        BYTEVAULT_MISSING_CATEGORY = 7,
        BYTEVAULT_INVALID_PAYLOAD = 8,
        BYTEVAULT_MISSING_CONTENT_TYPE = 9,
        BYTEVAULT_MISSING_CONTENT_LENGTH = 10,
        BYTEVAULT_MISSING_RECORD_NAME = 11,
        BYTEVAULT_INVALID_USER_TYPE = 12,
        BYTEVAULT_MAX_RECORD_SIZE_EXCEEDED = 13,
        BYTEVAULT_MAX_RECORD_COUNT_EXCEEDED = 14,
        BYTEVAULT_MISSING_CREATION_TIME = 15,
        BYTEVAULT_NOT_ALLOWED = 16,
        BYTEVAULT_INVALID_USER = 17,
        BYTEVAULT_NO_MATCHING_RECORD = 18,
        BYTEVAULT_INVALID_MAX_RECORD_SIZE = 19,
        BYTEVAULT_INVALID_MAX_RECORD_COUNT = 20,
        BYTEVAULT_INVALID_ACTION_TYPE = 21,
        BYTEVAULT_AUTHENTICATION_REQUIRED = 22,
        BYTEVAULT_INVALID_TOKEN_TYPE = 23,
        BYTEVAULT_INVALID_TRUSTED_SOURCE = 24,
        BYTEVAULT_INVALID_ADMIN_TYPE = 25,
        BYTEVAULT_NOT_INITIALIZED = 26,
        BYTEVAULT_ALREADY_INITIALIZED = 27,
        BYTEVAULT_INVALID_LASTUPDATETIME = 28,
        BYTEVAULT_INVALID_DATE_RANGE = 29,
        BYTEVAULT_INVALID_RECORD_NAME = 30,
        BYTEVAULT_INVALID_MAX_HISTORY_RECORD_COUNT = 31,
    }

    public enum ByteVaultComponentCommand : ushort
    {
        upsertAdmin = 1,
        getAdmin = 2,
        upsertContext = 3,
        getContexts = 4,
        deleteContext = 5,
        upsertCategory = 6,
        getCategories = 7,
        deleteCategory = 8,
        upsertRecord = 9,
        getRecord = 10,
        headRecord = 11,
        getRecordsInfo = 12,
        deleteRecord = 14,
        archiveRecords = 15,
        deleteHistoryRecord = 16,
        getHistoryRecord = 17,
        headHistoryRecord = 18,
        getArchivedRecordsInfo = 19,
        getAdminChangeHistory = 20,
        getDataRates = 21,
        getErrorRates = 22,
        restoreRecord = 23,
        getHistoryRecordsInfo = 24,
        getContext = 104,
        getCategory = 107,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => ByteVaultComponentBase.Id;
        public override string Name => ByteVaultComponentBase.Name;

        public virtual bool IsCommandSupported(ByteVaultComponentCommand command) => false;

        public class ByteVaultException : BlazeRpcException
        {
            public ByteVaultException(Error error) : base((ushort)error, null) { }
            public ByteVaultException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public ByteVaultException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public ByteVaultException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public ByteVaultException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public ByteVaultException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public ByteVaultException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public ByteVaultException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<UpsertAdminRequest, UpsertAdminResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.upsertAdmin,
                Name = "upsertAdmin",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.upsertAdmin),
                Func = async (req, ctx) => await UpsertAdminAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetAdminRequest, GetAdminResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getAdmin,
                Name = "getAdmin",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getAdmin),
                Func = async (req, ctx) => await GetAdminAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpsertContextRequest, UpsertContextResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.upsertContext,
                Name = "upsertContext",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.upsertContext),
                Func = async (req, ctx) => await UpsertContextAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetContextsRequest, GetContextsResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getContexts,
                Name = "getContexts",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getContexts),
                Func = async (req, ctx) => await GetContextsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DeleteContextRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.deleteContext,
                Name = "deleteContext",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.deleteContext),
                Func = async (req, ctx) => await DeleteContextAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpsertCategoryRequest, UpsertCategoryResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.upsertCategory,
                Name = "upsertCategory",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.upsertCategory),
                Func = async (req, ctx) => await UpsertCategoryAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetCategoriesRequest, GetCategoriesResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getCategories,
                Name = "getCategories",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getCategories),
                Func = async (req, ctx) => await GetCategoriesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DeleteCategoryRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.deleteCategory,
                Name = "deleteCategory",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.deleteCategory),
                Func = async (req, ctx) => await DeleteCategoryAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpsertRecordRequest, UpsertRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.upsertRecord,
                Name = "upsertRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.upsertRecord),
                Func = async (req, ctx) => await UpsertRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetRecordRequest, GetRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getRecord,
                Name = "getRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getRecord),
                Func = async (req, ctx) => await GetRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetRecordRequest, HeadRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.headRecord,
                Name = "headRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.headRecord),
                Func = async (req, ctx) => await HeadRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetRecordInfoRequest, GetRecordInfoResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getRecordsInfo,
                Name = "getRecordsInfo",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getRecordsInfo),
                Func = async (req, ctx) => await GetRecordsInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DeleteRecordRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.deleteRecord,
                Name = "deleteRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.deleteRecord),
                Func = async (req, ctx) => await DeleteRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ArchiveRecordsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.archiveRecords,
                Name = "archiveRecords",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.archiveRecords),
                Func = async (req, ctx) => await ArchiveRecordsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DeleteHistoryRecordRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.deleteHistoryRecord,
                Name = "deleteHistoryRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.deleteHistoryRecord),
                Func = async (req, ctx) => await DeleteHistoryRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetHistoryRecordRequest, GetHistoryRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getHistoryRecord,
                Name = "getHistoryRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getHistoryRecord),
                Func = async (req, ctx) => await GetHistoryRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetHistoryRecordRequest, HeadHistoryRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.headHistoryRecord,
                Name = "headHistoryRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.headHistoryRecord),
                Func = async (req, ctx) => await HeadHistoryRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetRecordInfoRequest, GetHistoryRecordInfoResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getArchivedRecordsInfo,
                Name = "getArchivedRecordsInfo",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getArchivedRecordsInfo),
                Func = async (req, ctx) => await GetArchivedRecordsInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetAdminChangeHistoryRequest, GetAdminChangeHistoryResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getAdminChangeHistory,
                Name = "getAdminChangeHistory",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getAdminChangeHistory),
                Func = async (req, ctx) => await GetAdminChangeHistoryAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetDataRatesRequest, GetDataRatesResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getDataRates,
                Name = "getDataRates",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getDataRates),
                Func = async (req, ctx) => await GetDataRatesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetErrorRatesRequest, GetErrorRatesResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getErrorRates,
                Name = "getErrorRates",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getErrorRates),
                Func = async (req, ctx) => await GetErrorRatesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetHistoryRecordRequest, UpsertRecordResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.restoreRecord,
                Name = "restoreRecord",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.restoreRecord),
                Func = async (req, ctx) => await RestoreRecordAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetRecordRequest, GetHistoryRecordInfoResponse, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getHistoryRecordsInfo,
                Name = "getHistoryRecordsInfo",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getHistoryRecordsInfo),
                Func = async (req, ctx) => await GetHistoryRecordsInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetContextsRequest, Context, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getContext,
                Name = "getContext",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getContext),
                Func = async (req, ctx) => await GetContextAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetCategoriesRequest, Category, EmptyMessage>()
            {
                Id = (ushort)ByteVaultComponentCommand.getCategory,
                Name = "getCategory",
                IsSupported = IsCommandSupported(ByteVaultComponentCommand.getCategory),
                Func = async (req, ctx) => await GetCategoryAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::upsertAdmin</b> command.<br/>
        /// Request type: <see cref="UpsertAdminRequest"/><br/>
        /// Response type: <see cref="UpsertAdminResponse"/><br/>
        /// </summary>
        public virtual Task<UpsertAdminResponse> UpsertAdminAsync(UpsertAdminRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getAdmin</b> command.<br/>
        /// Request type: <see cref="GetAdminRequest"/><br/>
        /// Response type: <see cref="GetAdminResponse"/><br/>
        /// </summary>
        public virtual Task<GetAdminResponse> GetAdminAsync(GetAdminRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::upsertContext</b> command.<br/>
        /// Request type: <see cref="UpsertContextRequest"/><br/>
        /// Response type: <see cref="UpsertContextResponse"/><br/>
        /// </summary>
        public virtual Task<UpsertContextResponse> UpsertContextAsync(UpsertContextRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getContexts</b> command.<br/>
        /// Request type: <see cref="GetContextsRequest"/><br/>
        /// Response type: <see cref="GetContextsResponse"/><br/>
        /// </summary>
        public virtual Task<GetContextsResponse> GetContextsAsync(GetContextsRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::deleteContext</b> command.<br/>
        /// Request type: <see cref="DeleteContextRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (void)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteContextAsync(DeleteContextRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::upsertCategory</b> command.<br/>
        /// Request type: <see cref="UpsertCategoryRequest"/><br/>
        /// Response type: <see cref="UpsertCategoryResponse"/><br/>
        /// </summary>
        public virtual Task<UpsertCategoryResponse> UpsertCategoryAsync(UpsertCategoryRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getCategories</b> command.<br/>
        /// Request type: <see cref="GetCategoriesRequest"/><br/>
        /// Response type: <see cref="GetCategoriesResponse"/><br/>
        /// </summary>
        public virtual Task<GetCategoriesResponse> GetCategoriesAsync(GetCategoriesRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::deleteCategory</b> command.<br/>
        /// Request type: <see cref="DeleteCategoryRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (void)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteCategoryAsync(DeleteCategoryRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::upsertRecord</b> command.<br/>
        /// Request type: <see cref="UpsertRecordRequest"/><br/>
        /// Response type: <see cref="UpsertRecordResponse"/><br/>
        /// </summary>
        public virtual Task<UpsertRecordResponse> UpsertRecordAsync(UpsertRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getRecord</b> command.<br/>
        /// Request type: <see cref="GetRecordRequest"/><br/>
        /// Response type: <see cref="GetRecordResponse"/><br/>
        /// </summary>
        public virtual Task<GetRecordResponse> GetRecordAsync(GetRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::headRecord</b> command.<br/>
        /// Request type: <see cref="GetRecordRequest"/><br/>
        /// Response type: <see cref="HeadRecordResponse"/><br/>
        /// </summary>
        public virtual Task<HeadRecordResponse> HeadRecordAsync(GetRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getRecordsInfo</b> command.<br/>
        /// Request type: <see cref="GetRecordInfoRequest"/><br/>
        /// Response type: <see cref="GetRecordInfoResponse"/><br/>
        /// </summary>
        public virtual Task<GetRecordInfoResponse> GetRecordsInfoAsync(GetRecordInfoRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::deleteRecord</b> command.<br/>
        /// Request type: <see cref="DeleteRecordRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (void)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteRecordAsync(DeleteRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::archiveRecords</b> command.<br/>
        /// Request type: <see cref="ArchiveRecordsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (void)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> ArchiveRecordsAsync(ArchiveRecordsRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::deleteHistoryRecord</b> command.<br/>
        /// Request type: <see cref="DeleteHistoryRecordRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (void)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteHistoryRecordAsync(DeleteHistoryRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getHistoryRecord</b> command.<br/>
        /// Request type: <see cref="GetHistoryRecordRequest"/><br/>
        /// Response type: <see cref="GetHistoryRecordResponse"/><br/>
        /// </summary>
        public virtual Task<GetHistoryRecordResponse> GetHistoryRecordAsync(GetHistoryRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::headHistoryRecord</b> command.<br/>
        /// Request type: <see cref="GetHistoryRecordRequest"/><br/>
        /// Response type: <see cref="HeadHistoryRecordResponse"/><br/>
        /// </summary>
        public virtual Task<HeadHistoryRecordResponse> HeadHistoryRecordAsync(GetHistoryRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getArchivedRecordsInfo</b> command.<br/>
        /// Request type: <see cref="GetRecordInfoRequest"/><br/>
        /// Response type: <see cref="GetHistoryRecordInfoResponse"/><br/>
        /// </summary>
        public virtual Task<GetHistoryRecordInfoResponse> GetArchivedRecordsInfoAsync(GetRecordInfoRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getAdminChangeHistory</b> command.<br/>
        /// Request type: <see cref="GetAdminChangeHistoryRequest"/><br/>
        /// Response type: <see cref="GetAdminChangeHistoryResponse"/><br/>
        /// </summary>
        public virtual Task<GetAdminChangeHistoryResponse> GetAdminChangeHistoryAsync(GetAdminChangeHistoryRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getDataRates</b> command.<br/>
        /// Request type: <see cref="GetDataRatesRequest"/><br/>
        /// Response type: <see cref="GetDataRatesResponse"/><br/>
        /// </summary>
        public virtual Task<GetDataRatesResponse> GetDataRatesAsync(GetDataRatesRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getErrorRates</b> command.<br/>
        /// Request type: <see cref="GetErrorRatesRequest"/><br/>
        /// Response type: <see cref="GetErrorRatesResponse"/><br/>
        /// </summary>
        public virtual Task<GetErrorRatesResponse> GetErrorRatesAsync(GetErrorRatesRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::restoreRecord</b> command.<br/>
        /// Request type: <see cref="GetHistoryRecordRequest"/><br/>
        /// Response type: <see cref="UpsertRecordResponse"/><br/>
        /// </summary>
        public virtual Task<UpsertRecordResponse> RestoreRecordAsync(GetHistoryRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getHistoryRecordsInfo</b> command.<br/>
        /// Request type: <see cref="GetRecordRequest"/><br/>
        /// Response type: <see cref="GetHistoryRecordInfoResponse"/><br/>
        /// </summary>
        public virtual Task<GetHistoryRecordInfoResponse> GetHistoryRecordsInfoAsync(GetRecordRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getContext</b> command.<br/>
        /// Request type: <see cref="GetContextsRequest"/><br/>
        /// Response type: <see cref="Context"/><br/>
        /// </summary>
        public virtual Task<Context> GetContextAsync(GetContextsRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ByteVaultComponent::getCategory</b> command.<br/>
        /// Request type: <see cref="GetCategoriesRequest"/><br/>
        /// Response type: <see cref="Category"/><br/>
        /// </summary>
        public virtual Task<Category> GetCategoryAsync(GetCategoriesRequest request, BlazeRpcContext context)
        {
            throw new ByteVaultException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
