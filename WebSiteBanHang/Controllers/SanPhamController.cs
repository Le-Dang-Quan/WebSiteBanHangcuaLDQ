using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using PagedList;
using System.Net;

namespace WebSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        // sử dụng partial view
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        //Xây dựng 1 action load sản phẩm theo mã loại sản phẩm và mã nhà sản xuất
        public ActionResult SanPham(int? MaLoaiSP, int? MaNSX, int? page)
        {
            //if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            //{
            //    return RedirectToAction("Index","Home");
            //}

            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*Load sản phẩm dựa theo 2 tiêu chí là Mã loại sản phẩm và mã nhà sản xuất (2 trường
            trong bảng sản phẩm */
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
            if (lstSP.Count() == 0)
            {
                //Thông báo nếu như không có sản phẩm đó
                return HttpNotFound();
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 3;
            //Tạo biến thứ 2: Số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult SanPham1()
        {
            // lấy dữ liệu nạp vào model (dữ liệu là tất cả các sản phẩm )
            var lstSanPhamAll = from spAll in db.SanPhams select spAll;
            ViewBag.ListAllSP = lstSanPhamAll;

            var lstSanPhamBoy = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.DaXoa == false);
            ViewBag.ListSPBoy = lstSanPhamBoy;

            var lstSanPhamGirl = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.DaXoa == false);
            ViewBag.ListSPGirl = lstSanPhamGirl;        

            return View();
        }
        public ActionResult SanPhamAoThun()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstAoThun = db.SanPhams.Where(n => n.PhanLoai == "ao" );
            ViewBag.ListAo = lstAoThun;
           
            return View();
        }
        public ActionResult SanPhamCaoGot()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstCaoGot = db.SanPhams.Where(n => n.PhanLoai == "caogot" && n.DaXoa == false);
            ViewBag.ListCaoGot = lstCaoGot;
            return View();
        }
        public ActionResult SanPhamViDa()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstViDa = db.SanPhams.Where(n => n.PhanLoai == "vi" && n.DaXoa == false);
            ViewBag.ListViDa = lstViDa;
            return View();
        }
        public ActionResult SanPhamDep()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstDep = db.SanPhams.Where(n => n.PhanLoai == "dep" && n.DaXoa == false);
            ViewBag.ListDep = lstDep;
            return View();
        }
        public ActionResult SanPhamVay()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstVay = db.SanPhams.Where(n => n.PhanLoai == "vay" && n.DaXoa == false);
            ViewBag.ListVay = lstVay;
            return View();
        }
        public ActionResult SanPhamQuan()
        {
            // kiểm tra - nếu ko đăng nhập thì nó sẽ link về trang chủ
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var lstQuan = db.SanPhams.Where(n => n.PhanLoai == "quan" && n.DaXoa == false);
            ViewBag.ListQuan = lstQuan;
            return View();
        }
        public ActionResult SanPham2()
        {
            return View();
        }
        public ActionResult SanPhamPartial()
        {
            return PartialView();
        }
    }
}