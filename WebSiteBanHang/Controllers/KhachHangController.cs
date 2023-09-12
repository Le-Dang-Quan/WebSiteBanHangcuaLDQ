using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        public ActionResult Index()
        {
            // truy vấn dữ liệu thông qua câu lệnh
            // đối tượng lstKH sẽ lấy toàn bộ dữ liệu từ bảng KhachHang

            // cách 1: lấy dữ liệu là 1 danh sách khách hàng
            //var lstKH = from KH in db.KhachHangs select KH;

            // cách 2: dùng phương thức hỗ trợ sẵn
            var lstKH = db.KhachHangs;
            return View(lstKH);
        }
        public ActionResult Index1()
        {
            var lstKH = from KH in db.KhachHangs select KH;
            return View(lstKH);
        }

        public ActionResult TruyVan1DoiTuong()
        {
            // cách 1: truy vấn 1 đối tượng bằng câu lệnh truy vấn
            // bước 1: lấy ra danh sách khách hàng
            var lstKH = from kh in db.KhachHangs where kh.MaKH == 1 select kh;
            // bước 2: lấy ra đối tượng kh
            //KhachHang k_hang = lstKH.FirstOrDefault();

            // lấy đối tượng khách hàng dựa trên phương thức hỗ trợ
            KhachHang k_hang = db.KhachHangs.SingleOrDefault(n => n.MaKH==1);
            return View(k_hang);
        }
        public ActionResult SortDuLieu()
        {
            // phg thức sắp xếp dữ liệu
            List<KhachHang> lstKH = db.KhachHangs.OrderByDescending(n => n.TenKH).ToList();     // giảm dần
            return View(lstKH);
        }
        public ActionResult GroupDuLieu()
        {
            // group dữ liệu
            List<ThanhVien> lstKH = db.ThanhViens.OrderBy(n => n.TaiKhoan).ToList();    // tăng dần
            return View(lstKH);
        }
    }
}