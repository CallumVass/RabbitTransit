using System;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using RabbitTransit.Contracts.Types;
using RabbitTransit.DataAccess;

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
            var model = _context.Products.Select(x => new UpdateStock
            {
                Id = x.Id,
                ProductNumber = x.ProductNumber,
                LastUpdated = DateTime.Now,
                StockLevel = x.StockLevel
            }).ToList();

            return Negotiate
                .WithAllowedMediaRange(new MediaRange("application/json"))
                .WithModel(model);
        }
    }
}