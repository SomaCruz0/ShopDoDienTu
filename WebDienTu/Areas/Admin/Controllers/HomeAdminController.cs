using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebDienTu.Data;
using WebDienTu.Models;
using X.PagedList;

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

        #region DanhMuc
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Loais.AsNoTracking().OrderBy(x => x.MaLoai);
            PagedList<Loai> lst = new PagedList<Loai>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.Loais.ToList();
            return View(lst);
        }

        [Route("ThemDanhMuc")]
        [HttpGet]
        public IActionResult ThemDanhMuc()
        {
            return View();
        }
        [Route("ThemDanhMuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDanhMuc(Loai danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.Loais.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(danhmuc);
        }
        #endregion


        #region Sản phẩm
        [Route("DanhSachSanPham")]
        public IActionResult DanhSachSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.HangHoas.AsNoTracking().OrderBy(x => x.MaHh);
            PagedList<HangHoa> lst = new PagedList<HangHoa>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.HangHoas.ToList();
            return View(lst);
        }

        [Route("ThemSanPham")]
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            ViewBag.MaLoai = new SelectList(db.Loais.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenCongTy");
            return View();
        }
        [Route("ThemSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(HangHoa sanpham)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("DanhSachSanPham");
            }
            return View(sanpham);
        }
        #endregion
    }
}
