using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;

namespace WebDienTu.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        ShopDienTuContext db = new ShopDienTuContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham()
        {
            var lstSanPham = db.Loais.ToList();
            return View(lstSanPham);
        }
    }
}
