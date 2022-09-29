using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Saga_BlockSeat.Consumers;
using Saga_BlockSeat.Infra;
using rabbitmq_message;
using rabbitmq_message.BusConfiguration;

namespace ms_order
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cfg =>
            {
                cfg.AddRequestClient<IStartOrder>();

                cfg.AddConsumer<StartOrderConsumer>();
                cfg.AddConsumer<OrderCancelledConsumer>();

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

            services.AddDbContext<OrderDbContext>
        (o => o.UseSqlServer(Configuration.
         GetConnectionString("OrderingDatabase")));

            services.AddSingleton<IOrderDataAccess, OrderDataAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
