﻿using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;
using WebDienTu.Helpers;
using WebDienTu.ViewModels;

namespace WebDienTu.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ShopDienTuContext db;

        public GioHangController(ShopDienTuContext context)
        {
            db = context;
        }
        const string CART_KEY = "MYCART";
        public List<GioHang> Cart => HttpContext.Session.Get<List<GioHang>>(CART_KEY) ?? new List<GioHang>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if(item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new GioHang
                {
                    MaHh = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(CART_KEY, gioHang);
            return RedirectToAction("Index");
        }
        public IActionResult RemoteCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if(item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(CART_KEY, gioHang);
            }

            return RedirectToAction("Index");
        }
    }
}
