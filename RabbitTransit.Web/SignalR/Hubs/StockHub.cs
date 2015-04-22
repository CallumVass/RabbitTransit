using Microsoft.AspNet.SignalR;
using RabbitTransit.Contracts.Types;

namespace RabbitTransit.Web.SignalR.Hubs
{
    public class StockHub : Hub
    {
        public void UpdateStock(UpdateStock result)
        {
            Clients.All.stockUpdated(result);
        }
    }
}
