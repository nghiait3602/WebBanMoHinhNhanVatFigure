using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        // GET: Admin/Role
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }
        //Thêm quyền 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if(ModelState.IsValid)
            {
                var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                role.Create(model);
                return RedirectToAction("Index");

            }
            return View(model);

        }
        public ActionResult Edit(string id)
        {
            var item = db.Roles.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                role.Update(model);
                return RedirectToAction("Index");

            }
            return View(model);

        }
    }
}