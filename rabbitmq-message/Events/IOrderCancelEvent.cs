using System;

namespace rabbitmq_message.Messages
{
    public interface IOrderCancelEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
