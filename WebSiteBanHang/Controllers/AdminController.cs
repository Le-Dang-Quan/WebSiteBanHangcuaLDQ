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
    public class AdminController : Controller
    {
        // GET: Admin
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        public ActionResult Index()
        {
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
            if (tv != null)
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