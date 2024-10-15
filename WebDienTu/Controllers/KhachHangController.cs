using Microsoft.AspNetCore.Mvc;
using WebDienTu.Data;
using WebDienTu.ViewModels;
using WebDienTu.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace WebDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ShopDienTuContext db;
        private readonly IMapper _mapper;

        public KhachHangController(ShopDienTuContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRamdomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;//sẽ xử lý khi dùng Mail để active
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }

                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.MaKh),
                                new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh),

								//claim - role động
								new Claim(ClaimTypes.Role, "Customer")
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            var data = db.KhachHangs.SingleOrDefault(p => p.MaKh == User.Identity.Name);
            if (data == null)
            {
                return Redirect("/404");
            }
            var result = new KhachHangVM
            {
                MaKh = data.MaKh,
                MatKhau = data.MatKhau,
                HoTen = data.HoTen,
                DiaChi = data.DiaChi,
                DienThoai = data.DienThoai,
                Email = data.Email
            };
            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
