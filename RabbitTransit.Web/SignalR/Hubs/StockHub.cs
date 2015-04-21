using Microsoft.AspNet.SignalR;
using RabbitTransit.Web.Nancy.ViewModels;

namespace RabbitTransit.Web.SignalR.Hubs
{
    public class StockHub : Hub
    {
        public void UpdateStock(UpdateStockViewModel result)
        {
            Clients.All.stockUpdated(result);
        }
    }
}
