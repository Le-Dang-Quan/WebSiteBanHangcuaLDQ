using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: ChiTietSanPham
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        public ActionResult ChiTietSP(int? id)
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            /*if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }*/

            var lstSanPhamAll = from spAll in db.SanPhams select spAll;
            ViewBag.ListAllSP = lstSanPhamAll;

            // kiểm tra tham số truyền vào có rỗng hay ko
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            // nếu ko thì truy xuất csdl lấy ra sp tương ứng
            var lstSanPhamOne = db.SanPhams.Where(n => n.MaSP == id && n.DaXoa == false);
            if (lstSanPhamOne == null)
            {
                // thông báo nếu như ko có sản phâm đó
                return HttpNotFound();
            }
            ViewBag.ListAllOne = lstSanPhamOne;

            /*var lstSanPhamOne = db.SanPhams.Where(n => n.MaSP == id && n.DaXoa == false);
            ViewBag.ListAllOne = lstSanPhamOne;*/

            return View();
        }
        public ActionResult ChiTietSanPhamPartial()
        {
            return PartialView();
        }
        public ActionResult SanPhamNoiBatPartial()
        {
            return PartialView();
        }
    }
}