namespace RabbitTransit.Contracts
{
    public interface IStockResult
    {
        int StockLevel { get; set; }
        string ProductNumber { get; set; }
    }
}
