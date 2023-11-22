using Microsoft.AspNetCore.Mvc;
using MySignalRWebApi.Models;

namespace MySignalRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _service;
        public SalesController(SaleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSale(Sale sale)
        {
            await _service.SaveSale(sale);
            return Ok(_service.GetSalesChartList());
        }

        [HttpGet]
        public IActionResult InitializeSale()
        {
            Random rnd = new Random();

            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (Product item in Enum.GetValues(typeof(Product)))
                {
                    var newSale = new Sale
                    {
                        ProductName = item,
                        SaleDate = DateTime.Now.AddDays(x),
                        Price = rnd.Next(1, 100)
                    };
                    _service.SaveSale(newSale).Wait();
                    Thread.Sleep(1000);
                }
            });

            return Ok("Satış dataları veritabanına kaydedildi");
        }
    }
}
