using System;

namespace RabbitTransit.Contracts.Types
{
    public class UpdateStock : IStockResult
    {
        public int Id { get; set; }
        public int StockLevel { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ProductNumber { get; set; }
    }
}
