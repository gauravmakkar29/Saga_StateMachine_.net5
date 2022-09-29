using System;

namespace rabbitmq_message.Messages
{
    public interface IOrderValidateEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
