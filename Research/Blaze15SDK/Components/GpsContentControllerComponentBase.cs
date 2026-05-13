using Blaze.Core;
using Blaze15SDK.Blaze.GpsContentController;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class GpsContentControllerComponentBase
{
    public const ushort Id = 27;
    public const string Name = "GpsContentControllerComponent";

    public enum Error : ushort {
        GPSCONTENTCONTROLLER_ERR_CONTENT_NOT_FOUND = 1,
    }

    public enum GpsContentControllerCommand : ushort
    {
        filePetition = 1,
        fetchContent = 2,
        showContent = 3,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => GpsContentControllerComponentBase.Id;
        public override string Name => GpsContentControllerComponentBase.Name;

        public virtual bool IsCommandSupported(GpsContentControllerCommand command) => false;

        public class GpsContentControllerException : BlazeRpcException
        {
            public GpsContentControllerException(Error error) : base((ushort)error, null) { }
            public GpsContentControllerException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public GpsContentControllerException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public GpsContentControllerException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public GpsContentControllerException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public GpsContentControllerException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public GpsContentControllerException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public GpsContentControllerException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<FilePetitionRequest, FilePetitionResponse, EmptyMessage>()
            {
                Id = (ushort)GpsContentControllerCommand.filePetition,
                Name = "filePetition",
                IsSupported = IsCommandSupported(GpsContentControllerCommand.filePetition),
                Func = async (req, ctx) => await FilePetitionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchContentRequest, FetchContentResponse, EmptyMessage>()
            {
                Id = (ushort)GpsContentControllerCommand.fetchContent,
                Name = "fetchContent",
                IsSupported = IsCommandSupported(GpsContentControllerCommand.fetchContent),
                Func = async (req, ctx) => await FetchContentAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ShowContentRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GpsContentControllerCommand.showContent,
                Name = "showContent",
                IsSupported = IsCommandSupported(GpsContentControllerCommand.showContent),
                Func = async (req, ctx) => await ShowContentAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>GpsContentControllerComponent::filePetition</b> command.<br/>
        /// Request type: <see cref="FilePetitionRequest"/><br/>
        /// Response type: <see cref="FilePetitionResponse"/><br/>
        /// </summary>
        public virtual Task<FilePetitionResponse> FilePetitionAsync(FilePetitionRequest request, BlazeRpcContext context)
        {
            throw new GpsContentControllerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GpsContentControllerComponent::fetchContent</b> command.<br/>
        /// Request type: <see cref="FetchContentRequest"/><br/>
        /// Response type: <see cref="FetchContentResponse"/><br/>
        /// </summary>
        public virtual Task<FetchContentResponse> FetchContentAsync(FetchContentRequest request, BlazeRpcContext context)
        {
            throw new GpsContentControllerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GpsContentControllerComponent::showContent</b> command.<br/>
        /// Request type: <see cref="ShowContentRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ShowContentAsync(ShowContentRequest request, BlazeRpcContext context)
        {
            throw new GpsContentControllerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
