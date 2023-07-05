using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Nhân Viên")]


    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImages.Where(x=> x.ProductId == id).ToList();
            return View(items);
        }
        public ActionResult AddImage(int productid,string url)
        {
            db.ProductImages.Add(new Models.EF.ProductImage
            {
                ProductId = productid,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id); 
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new {success =true});   
        }

    }
}