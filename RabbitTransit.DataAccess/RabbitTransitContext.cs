using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using RabbitTransit.Model;

namespace RabbitTransit.DataAccess
{
    public class RabbitTransitContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.ProductNumber).IsRequired();
            Property(x => x.StockLevel).IsRequired();
        }
    }
}
