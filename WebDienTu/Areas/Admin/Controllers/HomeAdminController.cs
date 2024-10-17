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

        #region Danh Mục
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

        [Route("SuaDanhMuc")]
        [HttpGet]
        public IActionResult SuaDanhMuc(int MaDM)
        {
            var danhmuc = db.Loais.Find(MaDM);
            return View(danhmuc);
        }
        [Route("SuaDanhMuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDanhMuc(Loai danhmuc)
        {
            db.Entry(danhmuc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        [Route("XoaDanhMuc")]
        [HttpGet]
        public IActionResult XoaDanhMuc(int MaDM)
        {
            TempData["Message"] = "";

            db.Remove(db.Loais.Find(MaDM));
            db.SaveChanges();

            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
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
            db.HangHoas.Add(sanpham);
            db.SaveChanges();
            return RedirectToAction("DanhSachSanPham");
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int MaSP)
        {
            ViewBag.MaLoai = new SelectList(db.Loais.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenCongTy");
            var sanpham = db.HangHoas.Find(MaSP);
            return View(sanpham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(HangHoa sanpham)
        {
            db.Entry(sanpham).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachSanPham","HomeAdmin");
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int MaSP)
        {
            TempData["Message"] = "";

            db.Remove(db.HangHoas.Find(MaSP));
            db.SaveChanges();

            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhSachSanPham", "HomeAdmin");
        }
        #endregion


        #region Khách hàng
        [Route("DanhSachKhachHang")]
        public IActionResult DanhSachKhachHang(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.KhachHangs.ToList();
            return View(lst);
        }

        [Route("ThemKhachHang")]
        [HttpGet]
        public IActionResult ThemKhachHang()
        {
            return View();
        }
        [Route("ThemKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKhachHang(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang");
        }

        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(string MaKH)
        {
            var khachhang = db.KhachHangs.Find(MaKH);
            return View(khachhang);
        }
        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(KhachHang kh)
        {
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang", "HomeAdmin");
        }

        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(string MaKH)
        {
            TempData["Message"] = "";

            db.Remove(db.KhachHangs.Find(MaKH));
            db.SaveChanges();

            TempData["Message"] = "Khách hàng đã được xóa";
            return RedirectToAction("DanhSachKhachHang", "HomeAdmin");
        }
        #endregion


        #region Nhân viên
        [Route("DanhSachNhanVien")]
        public IActionResult DanhSachNhanVien(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.NhanViens.AsNoTracking().OrderBy(x => x.MaNv);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.NhanViens.ToList();
            return View(lst);
        }

        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanvien)
        {
            db.NhanViens.Add(nhanvien);
            db.SaveChanges();
            return RedirectToAction("DanhSachNhanVien");
        }

        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string MaNV)
        {
            var nv = db.NhanViens.Find(MaNV);
            return View(nv);
        }
        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nhanvien)
        {
            db.Entry(nhanvien).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachNhanVien", "HomeAdmin");
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string MaNV)
        {
            TempData["Message"] = "";

            db.Remove(db.NhanViens.Find(MaNV));
            db.SaveChanges();

            TempData["Message"] = "Nhân viên đã được xóa";
            return RedirectToAction("DanhSachNhanVien", "HomeAdmin");
        }
        #endregion


        #region Hóa đơn
        [Route("DanhSachHoaDon")]
        public IActionResult DanhSachHoaDon(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.HoaDons.AsNoTracking().OrderBy(x => x.MaHd);
            PagedList<HoaDon> lst = new PagedList<HoaDon>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.HoaDons.ToList();
            return View(lst);
        }


        [Route("SuaHoaDon")]
        [HttpGet]
        public IActionResult SuaHoaDon(int HoaDon)
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList(), "MaKh", "HoTen");
            ViewBag.MaTrangThai = new SelectList(db.TrangThais.ToList(), "MaTrangThai", "TenTrangThai");
            ViewBag.MaNhanVien = new SelectList(db.NhanViens.ToList(), "MaNV", "HoTen");
            var sanpham = db.HoaDons.Find(HoaDon);
            return View(sanpham);
        }
        [Route("SuaHoaDon")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHoaDon(HoaDon hd)
        {
            db.Entry(hd).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachHoaDon", "HomeAdmin");
        }

        [Route("XoaHoaDon")]
        [HttpGet]
        public IActionResult XoaHoaDon(int MaHD)
        {
            TempData["Message"] = "";

            db.Remove(db.HoaDons.Find(MaHD));
            db.SaveChanges();

            TempData["Message"] = "Hóa đơn đã được xóa";
            return RedirectToAction("DanhSachHoaDon", "HomeAdmin");
        }
        #endregion

        #region Chi tiết hóa đơn
        [Route("DSCTHD")]
        public IActionResult DSCTHD(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.ChiTietHds.AsNoTracking().OrderBy(x => x.MaCt);
            PagedList<ChiTietHd> lst = new PagedList<ChiTietHd>(lstsanpham, pageNumber, pageSize);
            var lstSanPham = db.ChiTietHds.ToList();
            return View(lst);
        }

        [Route("SuaCTHD")]
        [HttpGet]
        public IActionResult SuaCTHD(int MaSP)
        {
            ViewBag.MaHd = new SelectList(db.HoaDons.ToList(), "MaHd", "HoTen");
            var sanpham = db.ChiTietHds.Find(MaSP);
            return View(sanpham);
        }
        [Route("SuaCTHD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTHD(ChiTietHd hd)
        {
            db.Entry(hd).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DSCTHD", "HomeAdmin");
        }

        [Route("XoaCTHD")]
        [HttpGet]
        public IActionResult XoaCTHD(int MaCTHD)
        {
            TempData["Message"] = "";

            db.Remove(db.ChiTietHds.Find(MaCTHD));
            db.SaveChanges();

            TempData["Message"] = "Hóa đơn đã được xóa";
            return RedirectToAction("DSCTHD", "HomeAdmin");
        }

        #endregion
    }
}
