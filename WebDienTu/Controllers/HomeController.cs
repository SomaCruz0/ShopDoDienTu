using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDienTu.Data;
using WebDienTu.Models;

namespace WebDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDienTuContext db; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ShopDienTuContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var products = db.HangHoas.ToList();
            return View(products);
        }

        [Route("/404")] 
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
