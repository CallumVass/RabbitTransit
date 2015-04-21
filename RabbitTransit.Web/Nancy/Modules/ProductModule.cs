using System;
using System.Linq;
using Nancy;
using RabbitTransit.DataAccess;
using RabbitTransit.Web.Nancy.ViewModels;

namespace RabbitTransit.Web.Nancy.Modules
{
    public class ProductModule : NancyModule
    {
        private readonly RabbitTransitContext _context;
        public ProductModule(RabbitTransitContext context)
            : base("/products")
        {
            _context = context;
            Get["/"] = Products;
        }

        private dynamic Products(dynamic o)
        {
            return _context.Products.Select(x => new UpdateStockViewModel
            {
                Id = x.Id,
                LastUpdated = DateTime.Now,
                StockLevel = x.StockLevel
            });
        }
    }
}