using Blaze.Core;
using Blaze15SDK.Blaze.XBLSystem;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class XblSystemConfigsComponentBase
{
    public const ushort Id = 1025;
    public const string Name = "XBLSystemConfigsComponent";

    public enum XblSystemConfigsCommand : ushort
    {
        validateStrings = 1,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => XblSystemConfigsComponentBase.Id;
        public override string Name => XblSystemConfigsComponentBase.Name;

        public virtual bool IsCommandSupported(XblSystemConfigsCommand command) => false;

        public class XblSystemConfigsException : BlazeRpcException
        {
            public XblSystemConfigsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public XblSystemConfigsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public XblSystemConfigsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public XblSystemConfigsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<ValidateStringsRequest, ValidateStringsResponse, EmptyMessage>()
            {
                Id = (ushort)XblSystemConfigsCommand.validateStrings,
                Name = "validateStrings",
                IsSupported = IsCommandSupported(XblSystemConfigsCommand.validateStrings),
                Func = async (req, ctx) => await ValidateStringsAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return errorCode.ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>XBLSystemConfigsComponent::validateStrings</b> command.<br/>
        /// Request type: <see cref="ValidateStringsRequest"/><br/>
        /// Response type: <see cref="ValidateStringsResponse"/><br/>
        /// </summary>
        public virtual Task<ValidateStringsResponse> ValidateStringsAsync(ValidateStringsRequest request, BlazeRpcContext context)
        {
            throw new XblSystemConfigsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
