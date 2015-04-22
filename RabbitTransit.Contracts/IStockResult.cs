using System;

namespace RabbitTransit.Contracts
{
    public interface IStockResult
    {
        int Id { get; set; }
        int StockLevel { get; set; }
        DateTime LastUpdated { get; set; }
        string ProductNumber { get; set; }
    }
}
