using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using rabbitmq_message.BusConfiguration;
using rabbitmq_saga.DbConfiguration;
using rabbitmq_saga.StateMachine;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace saga_machine
{
    class Program
    {
        static async Task Main(string[] args)
        {
              string connectionString = @"Server=(localdb)\MSSqlLocalDb;Database=OrderDb;Trusted_Connection=True;";

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddMassTransit(cfg =>
                   {
                       cfg.SetKebabCaseEndpointNameFormatter();
                       var entryAssembly = Assembly.GetEntryAssembly();

                       cfg.AddConsumers(entryAssembly);

                       cfg.AddSagaStateMachine<OrderStateMachine, OrderStateData>()
                        .EntityFrameworkRepository(r =>
                        {
                            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

                            r.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                            {
                                builder.UseSqlServer(connectionString, m =>
                                {
                                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                    m.MigrationsHistoryTable($"__{nameof(OrderStateDbContext)}");
                                });
                            });
                        });

                       cfg.UsingRabbitMq((ctx, cfg) =>
                       {
                           cfg.Host(BusConstants.RabbitMqUri, h =>
                           {
                               h.Username(BusConstants.UserName);
                               h.Password(BusConstants.Password);
                           });
                           cfg.ConfigureEndpoints(ctx);
                       });
                   });

                   services.AddMassTransitHostedService(true);
               });

#pragma warning disable CA1416 // Validate platform compatibility
            await builder.RunConsoleAsync();
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
   // public class MasstrainsitRabbitMqConfig: Imas
}
