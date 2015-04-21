using RabbitTransit.Model;

namespace RabbitTransit.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RabbitTransit.DataAccess.RabbitTransitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RabbitTransit.DataAccess.RabbitTransitContext context)
        {
            var random = new Random();

            Enumerable.Range(1, 1000)
                .Select(
                    (x, i) =>
                        new Product
                        {
                            Id = i < 1 ? i + 1 : i,
                            ProductNumber = string.Format("Product {0}", i < 1 ? i + 1 : i),
                            StockLevel = random.Next(0, 10)
                        }).ToList().ForEach(y => context.Products.AddOrUpdate(x => x.Id, y));
        }
    }
}
