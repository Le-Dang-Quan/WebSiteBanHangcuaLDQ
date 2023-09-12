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
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            // tìm kiếm theo tên sp
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;

            return View(lstSP.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize)) ;
        }

        [HttpPost]
        public ActionResult KQTimKiem(string sTuKhoa, int? page, FormCollection f)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            // tìm kiếm theo tên sp
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;

            return View(lstSP.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult TimKiemPartial()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
    }
}