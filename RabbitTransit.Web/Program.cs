using System;
using System.Data.Entity;
using System.Linq;
using MassTransit;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Nancy.Owin;
using Owin;
using RabbitTransit.Configuration;
using RabbitTransit.Contracts;
using RabbitTransit.DataAccess;
using RabbitTransit.Web.Nancy;

namespace RabbitTransit.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions();
            // add however many urls you want to here
            var urls = new[] { "http://127.0.0.1:3000", "http://localhost:3000" };
            urls.ToList().ForEach(options.Urls.Add);

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Running on: {0}", options.Urls.Aggregate((a, b) => string.Format("{0}, {1}", a, b)));

                var bus = BusInitializer.CreateBus("StockPublisher.Web",
                    x => x.Subscribe(subs => subs.Consumer<StockConsumer>().Permanent()));

                Console.ReadLine();
                bus.Dispose();
            }
        }
    }

    public class StockConsumer : Consumes<IStockResult>.Context
    {
        public void Consume(IConsumeContext<IStockResult> message)
        {
            var hubConnection = new HubConnection("http://localhost:3000");
            var hubProxy = hubConnection.CreateHubProxy("StockHub");

            hubConnection.Start().Wait();
            hubProxy.Invoke("UpdateStock", message.Message).Wait();

            var context = new RabbitTransitContext();
            var product = context.Products.Find(message.Message.Id);
            product.StockLevel = message.Message.StockLevel;
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .MapSignalR()
                .UseNancy(Configuration);
        }

        private static void Configuration(NancyOptions options)
        {
            options.Bootstrapper = new Bootstrapper();
        }
    }
}
