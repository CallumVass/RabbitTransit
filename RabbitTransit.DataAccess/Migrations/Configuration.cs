using RabbitTransit.Model;

namespace RabbitTransit.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var random = new Random();

            Enumerable.Range(1, 1000)
                .Select(
                    (x, i) =>
                        new Product
                        {
                            Id = i,
                            ProductNumber = string.Format("Product {0}", i),
                            StockLevel = random.Next(0, 10)
                        }).ToList().ForEach(y => context.Products.AddOrUpdate(x => x.Id, y));
        }
    }
}