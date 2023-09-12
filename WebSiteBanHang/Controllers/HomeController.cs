using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using PagedList;

namespace WebSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        public ActionResult Index()
        {
            var lstSanPhamAll = from spAll in db.SanPhams select spAll;
            ViewBag.ListAllSP = lstSanPhamAll;

            // Lần lượt tạo các viewbag để lấy list sản phẩm từ cơ sở dữ liệu
            //List Quần mới nhất
            var lstQuan = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListQuan = lstQuan;

            //List Áo mới nhất
            var lstAo = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListAo = lstAo;

            //List Giay dép bảng mới nhất
            var lstGiay = db.SanPhams.Where(n => n.MaLoaiSP == 4 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListGiay = lstGiay;

            //List Váy mới nhất
            var lstVay = db.SanPhams.Where(n => n.MaLoaiSP == 5 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListVay = lstVay;

            //List guốc mới nhất
            var lstGuoc = db.SanPhams.Where(n => n.MaLoaiSP == 6 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListGuoc = lstGuoc;

            //List Ví bảng mới nhất
            var lstVi = db.SanPhams.Where(n => n.MaLoaiSP == 7 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListVi = lstVi;

            return View();
        }
        public ActionResult MenuPartialView()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            // kiểm tra về captcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                // thêm khách hàng vào csdl
                db.ThanhViens.Add(tv);
                db.SaveChanges();

                ViewBag.ThongBao = "Đăng ký thành công";
                return View();

                
            }
            ViewBag.ThongBao = "Sai mã captcha. Vui lòng thử lại!";

            
            /*string sTaiKhoan = f["TaiKhoan"].ToString();
            string sEmail = f["Email"].ToString();
            ThanhVien tv1 = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan || n.Email == sEmail);
            if (tv1 != null)
            {
                if (this.IsCaptchaValid("Captcha is not valid"))
                {
                    ThanhVien tv;
                    // thêm khách hàng vào csdl
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();

                    ViewBag.ThongBao = "Đăng ký thành công";
                    return View();
                }
                ViewBag.ThongBao = "Sai mã captcha. Vui lòng thử lại!";
            }
            ViewBag.ThongBao = "Tài Khoản đã tồn tại!";*/

            return View();
        }

        // xấy dựng action đăng nhập
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            // kiểm tra tên đăng nhập và mật khẩu
            string sTaiKhoan = f["txtDangNhap"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if(tv != null)
            {
                Session["TaiKhoan"] = tv;
                return Content("<script>window.location.reload();</script>");
            }

            return Content("Tên đăng nhập hoặc mật khẩu KHÔNG chính xác!");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}