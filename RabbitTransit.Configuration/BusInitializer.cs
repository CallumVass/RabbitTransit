using System;
using MassTransit;
using MassTransit.BusConfigurators;

namespace RabbitTransit.Configuration
{
    public class BusInitializer
    {
        public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
        {
            //Log4NetLogger.Use();
            var bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://cv-e6540/" + queueName);
                moreInitialization(x);
            });

            return bus;
        }
    }
}
