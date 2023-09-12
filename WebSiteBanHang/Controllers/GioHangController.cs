using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        // lấy giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {
            // giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang == null)
            {
                // nếu session giỏ hàng  chưa tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            // nếu đã tồn tại thì lấy giỏ hàng ra
            return lstGioHang;
        }
        // thêm giỏ hàng thông thường (load lại trang)
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            // kiểm tra sản phẩm có tồn tại trong CSDL hay ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp 1: sp đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck != null)
            {
                // kiểm tra số lượng tồn trc khi cho khách hàng mua hàng
                if(sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }    
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }
        // tính tổng số lượng
        public double TinhTongSoLuong()
        {
            // lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        // tính tổng tiền
        public decimal TinhTongTien()
        {
            // lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult GioHangPartial()
        {
            if(TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TinhTongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TinhTongTien = TinhTongTien();

            return PartialView();
        }

         // GET: GioHang
        public ActionResult XemGioHang()
        {
            // lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        // chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(int MaSP)
        {
            // kiểm tra session giỏ hàng tồn tại chưa
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            // kiểm tra sản phẩm có tồn tại trong CSDL hay ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra xem sp đó có tồn tại trong giỏ hàng hay không?
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // lấy list giỏ hàng để tạo giao diện
            ViewBag.GioHang = lstGioHang;
            // nếu tồn tại rồi
            return View(spCheck);
        }

        // xử lý nút cập nhạt giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang ItemGH)
        {
            // kiểm tra số lượng tồn
            SanPham spCheck = db.SanPhams.Single(n => n.MaSP == ItemGH.MaSP);
            if(spCheck.SoLuongTon < ItemGH.SoLuong)
            {
                return View("ThongBao");
            }
            // thay đổi cập nhật số lượng trong session giỏ hàng
            // bước 1: lấy List<GioHang> từ session["GioHang"]
            List<ItemGioHang> lstGH = LayGioHang();

            // bước 2: lấy sp cần cập nhật từ trong list<GioHang> ra
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.MaSP == ItemGH.MaSP);

            // bước 3: tiến hành update lại số lượng và tính lại thành tiền
            itemGHUpdate.SoLuong = ItemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            itemGHUpdate.Size = ItemGH.Size;
            
            return RedirectToAction("XemGioHang");
        }

        // xóa giỏ hàng
        public ActionResult XoaGioHang(int MaSP)
        {
            // kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong CSDL hay ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra xem sp đó có tồn tại trong giỏ hàng hay không?
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);

            return RedirectToAction("XemGioHang");
        }

        // xây dựng chức năng đặt hàng
        public ActionResult DatHang(KhachHang kh)
        {
            // kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // ----------

            KhachHang khang = new KhachHang();
            if(Session["TaiKhoan"] == null)
            {
                // thêm khách hàng vào bảng KhachHang khi khách hàng chưa có tài khoản
                khang = kh;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                // đối với khách hàng là thành viên
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                khang.TenKH = tv.HoTen;
                khang.DiaChi = tv.DiaChi;
                khang.Email = tv.Email;
                khang.SoDienThoai = tv.SoDienThoai;
                khang.MaThanhVien = tv.MaLoaiTV;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }

            // thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khang.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            // thêm chi tiết đơn đặt hàng
            List<ItemGioHang> lstGH = LayGioHang();
            foreach(var item in lstGH)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }
    }
}