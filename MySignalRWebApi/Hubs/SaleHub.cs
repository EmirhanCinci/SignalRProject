using Microsoft.AspNetCore.SignalR;
using MySignalRWebApi.Models;

namespace MySignalRWebApi.Hubs
{
    public class SaleHub : Hub
    {
        private readonly SaleService _service;

        public SaleHub(SaleService service)
        {
            _service = service;
        }

        public async Task GetSaleList()
        {
            await Clients.All.SendAsync("ReceiveSaleList", _service.GetSalesChartList());
        }
    }
}
