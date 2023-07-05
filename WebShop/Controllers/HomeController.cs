using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.Models.EF;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Sub() 
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Sub(Subscribe req)
        {
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_sendEmail/theodoi.html"));

            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe
                {
                    Email = req.Email,
                    CreateDate = DateTime.Now,
                });
                db.SaveChanges();
                WebShop.Models.Commons.Format.SendMail("NTVFIGUREShop", "Thông báo bạn đã theo dõi!", contentCustomer, req.Email);
                return Json(new {Success = true });

            }
            return View("Index", req);


        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Chat()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}