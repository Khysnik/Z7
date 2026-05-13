using Blaze.Core;
using Blaze15SDK.Blaze.CensusData;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class CensusDataComponentBase
{
    public const ushort Id = 10;
    public const string Name = "CensusDataComponent";

    public enum Error : ushort
    {
        CENSUSDATA_ERR_PLAYER_ALREADY_SUBSCRIBED = 1,
        CENSUSDATA_ERR_PLAYER_NOT_SUBSCRIBED = 2,
    }

    public enum CensusDataCommand : ushort
    {
        subscribeToCensusData = 1,
        unsubscribeFromCensusData = 2,
        getRegionCounts = 3,
        getLatestCensusData = 4,
        subscribeToCensusDataUpdates = 5,
    }

    public enum CensusDataNotification : ushort
    {
        NotifyServerCensusData = 1,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => CensusDataComponentBase.Id;
        public override string Name => CensusDataComponentBase.Name;

        public virtual bool IsCommandSupported(CensusDataCommand command) => false;

        public class CensusDataException : BlazeRpcException
        {
            public CensusDataException(Error error) : base((ushort)error, null) { }
            public CensusDataException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public CensusDataException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public CensusDataException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public CensusDataException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public CensusDataException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public CensusDataException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public CensusDataException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)CensusDataCommand.subscribeToCensusData,
                Name = "subscribeToCensusData",
                IsSupported = IsCommandSupported(CensusDataCommand.subscribeToCensusData),
                Func = async (req, ctx) => await SubscribeToCensusDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)CensusDataCommand.unsubscribeFromCensusData,
                Name = "unsubscribeFromCensusData",
                IsSupported = IsCommandSupported(CensusDataCommand.unsubscribeFromCensusData),
                Func = async (req, ctx) => await UnsubscribeFromCensusDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, RegionCounts, EmptyMessage>()
            {
                Id = (ushort)CensusDataCommand.getRegionCounts,
                Name = "getRegionCounts",
                IsSupported = IsCommandSupported(CensusDataCommand.getRegionCounts),
                Func = async (req, ctx) => await GetRegionCountsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, NotifyServerCensusData, EmptyMessage>()
            {
                Id = (ushort)CensusDataCommand.getLatestCensusData,
                Name = "getLatestCensusData",
                IsSupported = IsCommandSupported(CensusDataCommand.getLatestCensusData),
                Func = async (req, ctx) => await GetLatestCensusDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SubscribeToCensusDataUpdatesRequest, SubscribeToCensusDataUpdatesResponse, EmptyMessage>()
            {
                Id = (ushort)CensusDataCommand.subscribeToCensusDataUpdates,
                Name = "subscribeToCensusDataUpdates",
                IsSupported = IsCommandSupported(CensusDataCommand.subscribeToCensusDataUpdates),
                Func = async (req, ctx) => await SubscribeToCensusDataUpdatesAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>CensusDataComponent::subscribeToCensusData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SubscribeToCensusDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new CensusDataException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>CensusDataComponent::unsubscribeFromCensusData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UnsubscribeFromCensusDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new CensusDataException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>CensusDataComponent::getRegionCounts</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="RegionCounts"/><br/>
        /// </summary>
        public virtual Task<RegionCounts> GetRegionCountsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new CensusDataException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>CensusDataComponent::getLatestCensusData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="NotifyServerCensusData"/><br/>
        /// </summary>
        public virtual Task<NotifyServerCensusData> GetLatestCensusDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new CensusDataException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>CensusDataComponent::subscribeToCensusDataUpdates</b> command.<br/>
        /// Request type: <see cref="SubscribeToCensusDataUpdatesRequest"/><br/>
        /// Response type: <see cref="SubscribeToCensusDataUpdatesResponse"/><br/>
        /// </summary>
        public virtual Task<SubscribeToCensusDataUpdatesResponse> SubscribeToCensusDataUpdatesAsync(SubscribeToCensusDataUpdatesRequest request, BlazeRpcContext context)
        {
            throw new CensusDataException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>CensusDataComponent::NotifyServerCensusData</b> notification.<br/>
        /// Notification type: <see cref="NotifyServerCensusData"/><br/>
        /// </summary>
        public static Task NotifyServerCensusDataAsync(BlazeRpcConnection connection, NotifyServerCensusData notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = CensusDataComponentBase.Id;
                frame.Command = (ushort)CensusDataNotification.NotifyServerCensusData;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

    }

}
