using System;
using RabbitTransit.Contracts;

namespace RabbitTransit.Web.Nancy.ViewModels
{
    public class UpdateStockViewModel : IStockResult
    {
        public int Id { get; set; }
        public int StockLevel { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
