using System;
using System.Threading;
using MassTransit;
using RabbitTransit.Configuration;
using RabbitTransit.Contracts.Types;

namespace RabbitTransit.StockReplenisher
{
    class Program
    {
        private static Timer _timer;

        static void Main(string[] args)
        {
            // This is just a background task checking/updating stock all the time (imagine)
            var pubBus = BusInitializer.CreateBus("StockPublisher", x => { });
            var random = new Random();
            //need to store it in a field to prevent GC
            _timer = new Timer(x =>
            {
                var updateStock = new UpdateStock
                {
                    Id = random.Next(1, 1000),
                    StockLevel = random.Next(0, 10),
                    LastUpdated = DateTime.Now
                };

                pubBus.Publish(updateStock, y => y.SetDeliveryMode(DeliveryMode.Persistent));

            }, null, 0, random.Next(1000));

            Console.ReadLine();
        }
    }
}
