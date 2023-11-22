using Microsoft.AspNetCore.Mvc;

namespace MySignalRProject.MvcUI.Controllers
{
    public class SaleShow : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
