using System;
using System.Threading;
using MassTransit;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using RabbitTransit.Configuration;

namespace RabbitTransit.Web.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var bus = BusInitializer.CreateBus("StockPublisher", x => { });
            container.Register(bus);


            new Timer(x => bus.Publish("test", y => y.SetDeliveryMode(DeliveryMode.Persistent)), null, 0, new Random().Next(100));

            base.ApplicationStartup(container, pipelines);
        }
    }
}
