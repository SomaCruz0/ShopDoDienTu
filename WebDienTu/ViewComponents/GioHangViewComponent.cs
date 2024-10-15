using Microsoft.AspNetCore.Mvc;
using WebDienTu.Helpers;
using WebDienTu.ViewModels;

namespace WebDienTu.ViewComponents
{
    public class GioHangViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<GioHang>>(MySetting.CART_KEY) ?? new List<GioHang>();

            return View("GioHangPanel", new GioHangModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
