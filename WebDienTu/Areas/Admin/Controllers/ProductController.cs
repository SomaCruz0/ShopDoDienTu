using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.HangHoas.OrderByDescending(p => p.MaHh).Include(p => p.MaLoaiNavigation).ToListAsync());
        }
    }
}
