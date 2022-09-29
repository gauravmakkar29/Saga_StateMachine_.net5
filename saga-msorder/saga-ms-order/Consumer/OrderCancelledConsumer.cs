using MassTransit;
using Microsoft.Extensions.Logging;
using Saga_BlockSeat.Infra;
using rabbitmq_message.Messages;
using System;
using System.Threading.Tasks;

namespace Saga_BlockSeat.Consumers
{
    public class OrderCancelledConsumer : IConsumer<IOrderCancelEvent>
    {
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderCancelledConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<IOrderCancelEvent> context)
        {
            var data = context.Message;
            _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
