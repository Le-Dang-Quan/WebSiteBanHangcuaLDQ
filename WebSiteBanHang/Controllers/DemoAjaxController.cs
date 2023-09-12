using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class DemoAjaxController : Controller
    {
        // GET: DemoAjax
        QuanLyBanHangEntities5 db = new QuanLyBanHangEntities5();
        public ActionResult DemoAjax()
        {
            return View();
        }
        // xử lý ajax action link
        public ActionResult LoadAjaxActionLink()
        {
            System.Threading.Thread.Sleep(3000);
            return Content("hello ajax");
        }

        // xử lý ajax BeginForm
        public ActionResult LoadAjaxBeginForm(FormCollection f)
        {
            System.Threading.Thread.Sleep(3000);
            string KQ = f["txt1"].ToString();
            return Content(KQ);
        }
        // xử lý ajax JQuery
        public ActionResult LoadAjaxJQuery(int a, int b)
        {
            System.Threading.Thread.Sleep(1000);
            return Content((a + b).ToString());
        }

        // sử dụng load ajax trả về kết quả 1 partialview
        public ActionResult SanPhamPartial_Ajax_AoThun()
        {
            return PartialView();
        }

    }
}