using System;

namespace rabbitmq_message.StateMachine.Messages
{
    public interface IOrderStartedEvent
    {
        public Guid OrderId { get; set; }
        public string PaymentCardNumber { get; set; }
        public string ProductName { get; set; }
    }
}
