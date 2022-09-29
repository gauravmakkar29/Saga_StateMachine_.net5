using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace rabbitmq_message.BusConfiguration
{
    public class RabbitMqBus
    {
        //public static IBusControl ConfigureBus(IServiceProvider provider, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
        // registrationAction = null)
        //{

        //    return Bus.Factory.CreateUsingRabbitMq(cfg =>
        //    {
        //        cfg.Host(new Uri(BusConstants.RabbitMqUri), hst =>
        //       {
        //           hst.Username(BusConstants.UserName);
        //           hst.Password(BusConstants.Password);
        //       });

        //        cfg.ReceiveEndpoint(BusConstants.StartOrderTranastionQueue, e =>
        //        {
                   
        //        });
        //        cfg.ConfigureEndpoints(.ConfigureEndpoints();

        //        registrationAction?.Invoke(cfg, host);
        //    });
        //}
    }
}
