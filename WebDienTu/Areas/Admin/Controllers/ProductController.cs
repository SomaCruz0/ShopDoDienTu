using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;

namespace WebDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class ProductController : Controller
    {
        private readonly ShopDienTuContext _dataContext;

        public ProductController(ShopDienTuContext context)
        {
            _dataContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
