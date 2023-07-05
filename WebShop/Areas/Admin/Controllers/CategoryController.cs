using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.Models.Commons;
using WebShop.Models.EF;

namespace WebShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Nhân Viên")]


    public class CategoryController : Controller
    {
        // GET: Admin/Category
      private  ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var item = db.Categories.ToList();
            return View(item);
        }
        public ActionResult Add() { 
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = Models.Commons.Filter.FilterChar(model.Title);
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = Models.Commons.Filter.FilterChar(model.Title);
                db.Entry(model).Property(e => e.Title).IsModified = true;
                db.Entry(model).Property(e => e.Description).IsModified = true;
                db.Entry(model).Property(e => e.SeoKeywords).IsModified = true;
                db.Entry(model).Property(e => e.SeoDescription).IsModified = true;
                db.Entry(model).Property(e => e.Alias).IsModified = true;
                db.Entry(model).Property(e => e.Position).IsModified = true;
                db.Entry(model).Property(e => e.SeoTitle).IsModified = true;
                db.Entry(model).Property(e => e.ModifiedDate).IsModified = true;
                db.Entry(model).Property(e => e.ModifiedBy).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if(item != null)
            {
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });

        }
    }
}