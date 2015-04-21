using System;
using System.IO;
using System.Threading;
using MassTransit;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using RabbitTransit.Configuration;
using RabbitTransit.DataAccess;
using RabbitTransit.Web.Nancy.ViewModels;

namespace RabbitTransit.Web.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("build"));
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = new[]
                {
                    typeof(ViewProcessor),
                    typeof(JsonProcessor),
                    typeof(XmlProcessor)
                });
            }
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new SelfHostRootPathProvider(); }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat(@"Nancy/Views/", viewName));
            var bus = BusInitializer.CreateBus("StockPublisher", x => { });
            container.Register(bus);
            var random = new Random();
            // This is just a background task checking/updating stock all the time (imagine)
            new Timer(x =>
            {
                var updateStock = new UpdateStockViewModel
                {
                    Id = random.Next(1, 1000),
                    StockLevel = random.Next(0, 10),
                    LastUpdated = DateTime.Now
                };

                bus.Publish(updateStock, y => y.SetDeliveryMode(DeliveryMode.Persistent));

            }, null, 0, random.Next(100));

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register(new RabbitTransitContext());
            base.ConfigureRequestContainer(container, context);
        }
    }

    public class SelfHostRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return StaticConfiguration.IsRunningDebug
                ? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."))
                : AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
