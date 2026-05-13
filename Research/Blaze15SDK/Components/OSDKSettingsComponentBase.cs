using Blaze.Core;
using Blaze15SDK.Blaze.OSDKSettings;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class OSDKSettingsComponentBase
{
    public const ushort Id = 2249; // custom/201 = (1 << 11) | 201 = 0x08C9
    public const string Name = "OSDKSettingsComponent";

    public enum OSDKSettingsComponentCommand : ushort
    {
        fetchSettings = 1,
        fetchSettingsGroups = 2,
    }

    public enum OSDKSettingsComponentNotification : ushort
    {
    }

    public class Server : BlazeComponent {
        public override ushort Id => OSDKSettingsComponentBase.Id;
        public override string Name => OSDKSettingsComponentBase.Name;

        public virtual bool IsCommandSupported(OSDKSettingsComponentCommand command) => false;

        public class OSDKSettingsException : BlazeRpcException
        {
            public OSDKSettingsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public OSDKSettingsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public OSDKSettingsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public OSDKSettingsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            RegisterCommand(new RpcCommandFunc<EmptyMessage, FetchSettingsResponse, EmptyMessage>()
            {
                Id = (ushort)OSDKSettingsComponentCommand.fetchSettings,
                Name = "fetchSettings",
                IsSupported = IsCommandSupported(OSDKSettingsComponentCommand.fetchSettings),
                Func = async (req, ctx) => await FetchSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, FetchSettingsGroupsResponse, EmptyMessage>()
            {
                Id = (ushort)OSDKSettingsComponentCommand.fetchSettingsGroups,
                Name = "fetchSettingsGroups",
                IsSupported = IsCommandSupported(OSDKSettingsComponentCommand.fetchSettingsGroups),
                Func = async (req, ctx) => await FetchSettingsGroupsAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return $"OSDK_SETTINGS_ERR_{errorCode}";
        }

        /// <summary>
        /// This method is called when server receives a <b>OSDKSettingsComponent::fetchSettings</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="FetchSettingsResponse"/><br/>
        /// </summary>
        public virtual Task<FetchSettingsResponse> FetchSettingsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new OSDKSettingsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>OSDKSettingsComponent::fetchSettingsGroups</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="FetchSettingsGroupsResponse"/><br/>
        /// </summary>
        public virtual Task<FetchSettingsGroupsResponse> FetchSettingsGroupsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new OSDKSettingsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
