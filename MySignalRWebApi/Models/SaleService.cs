using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MySignalRWebApi.Hubs;

namespace MySignalRWebApi.Models
{
    public class SaleService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<SaleHub> _hubContext;

        public SaleService(AppDbContext context, IHubContext<SaleHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Sale> GetList()
        {
            return _context.Sales.AsQueryable();
        }

        public async Task SaveSale(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveSaleList", GetSalesChartList());
        }

        public List<SaleChart> GetSalesChartList()
        {
            List<SaleChart> salCharts = new List<SaleChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3],[4],[5]  FROM(select[ProductName],[Price], Cast([SaleDate] as date) as tarih FROM Sales) as covidT PIVOT (SUM(Price) For ProductName IN([1],[2],[3],[4],[5]) ) as ptable order by tarih asc";
                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SaleChart cc = new SaleChart();
                        cc.SaleDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (DBNull.Value.Equals(reader[x]))
                            {
                                cc.Prices.Add(0);
                            }
                            else
                            {
                                cc.Prices.Add(reader.GetInt32(x));
                            }
                        });
                        salCharts.Add(cc);
                    }
                }
                _context.Database.CloseConnection();
                return salCharts;
            }
        }
    }
}
