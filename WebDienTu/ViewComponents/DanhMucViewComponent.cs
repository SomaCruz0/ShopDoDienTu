using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;
using WebDienTu.ViewModels;

namespace WebDienTu.ViewComponents
{
    public class DanhMucViewComponent : ViewComponent
    {
        private readonly ShopDienTuContext db;

        public DanhMucViewComponent(ShopDienTuContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new DanhMucVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View("Default",data);
        }
    }
}
