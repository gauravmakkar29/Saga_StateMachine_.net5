using MassTransit;
using Microsoft.Extensions.Logging;
using rabbitmq_message;
using rabbitmq_message.StateMachine.Messages;
using rabbitmq_saga.StateMachine;
using System.Threading.Tasks;

namespace Saga_BlockSeat.Consumers
{
    public class StartOrderConsumer :
        IConsumer<IStartOrder>
    {
        readonly ILogger<StartOrderConsumer> _logger;
        public StartOrderConsumer()
        {
        }

        public StartOrderConsumer(ILogger<StartOrderConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IStartOrder> context)
        {
            _logger.LogInformation("--Application Event-- Order Transation Started and event published: {OrderId}", context.Message.OrderId);

            await context.Publish<IOrderStartedEvent>(new
            {
                context.Message.OrderId
                ,context.Message.PaymentCardNumber
                ,context.Message.ProductName
            });

        }
    }
}
