using Blaze.Core;
using Blaze15SDK.Blaze.Example;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class ExampleComponentBase
{
    public const ushort Id = 3;
    public const string Name = "ExampleComponent";

    public enum Error : ushort {
        EXAMPLE_ERR_UNKNOWN = 1,
    }

    public enum ExampleComponentCommand : ushort
    {
        poke = 1,
        rpcPassthrough = 2,
        requestToUserSessionOwner = 3,
    }

    public enum ExampleComponentMasterCommand : ushort
    {
        pokeMaster = 1,
        rpcPassthroughMaster = 2,
    }

    public enum ExampleComponentNotification : ushort
    {
    }

    public class Server : BlazeComponent {
        public override ushort Id => ExampleComponentBase.Id;
        public override string Name => ExampleComponentBase.Name;

        public virtual bool IsCommandSupported(ExampleComponentCommand command) => false;

        public class ExampleException : BlazeRpcException
        {
            public ExampleException(Error error) : base((ushort)error, null) { }
            public ExampleException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public ExampleException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public ExampleException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public ExampleException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public ExampleException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public ExampleException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public ExampleException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            RegisterCommand(new RpcCommandFunc<ExampleRequest, ExampleResponse, ExampleError>()
            {
                Id = (ushort)ExampleComponentCommand.poke,
                Name = "poke",
                IsSupported = IsCommandSupported(ExampleComponentCommand.poke),
                Func = async (req, ctx) => await PokeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ExampleComponentCommand.rpcPassthrough,
                Name = "rpcPassthrough",
                IsSupported = IsCommandSupported(ExampleComponentCommand.rpcPassthrough),
                Func = async (req, ctx) => await RpcPassthroughAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ExampleComponentCommand.requestToUserSessionOwner,
                Name = "requestToUserSessionOwner",
                IsSupported = IsCommandSupported(ExampleComponentCommand.requestToUserSessionOwner),
                Func = async (req, ctx) => await RequestToUserSessionOwnerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ExampleRequest, ExampleResponse, EmptyMessage>()
            {
                Id = (ushort)ExampleComponentMasterCommand.pokeMaster,
                Name = "pokeMaster",
                IsSupported = true,
                Func = async (req, ctx) => await PokeMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ExampleComponentMasterCommand.rpcPassthroughMaster,
                Name = "rpcPassthroughMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RpcPassthroughMasterAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        /// <summary>
        /// This method is called when server receives a <b>ExampleComponent::poke</b> command.<br/>
        /// Request type: <see cref="ExampleRequest"/><br/>
        /// Response type: <see cref="ExampleResponse"/><br/>
        /// Error response type: <see cref="ExampleError"/><br/>
        /// </summary>
        public virtual Task<ExampleResponse> PokeAsync(ExampleRequest request, BlazeRpcContext context)
        {
            throw new ExampleException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ExampleComponent::rpcPassthrough</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RpcPassthroughAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ExampleException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ExampleComponent::requestToUserSessionOwner</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestToUserSessionOwnerAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ExampleException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ExampleComponent::pokeMaster</b> master command.<br/>
        /// Request type: <see cref="ExampleRequest"/><br/>
        /// Response type: <see cref="ExampleResponse"/><br/>
        /// </summary>
        public virtual Task<ExampleResponse> PokeMasterAsync(ExampleRequest request, BlazeRpcContext context)
        {
            throw new ExampleException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ExampleComponent::rpcPassthroughMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RpcPassthroughMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ExampleException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
