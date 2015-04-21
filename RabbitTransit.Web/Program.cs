using System;
using System.Linq;
using Microsoft.Owin.Hosting;
using Nancy.Owin;
using Owin;
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

                Console.ReadLine();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(Configuration);
        }

        private static void Configuration(NancyOptions options)
        {
            options.Bootstrapper = new Bootstrapper();
        }
    }
}
