using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int? id)
        {
            var items= db.Products.ToList();

            if (id != null)
            {
                items= items.Where(x => x.Id == id).ToList();
            }
            return View(items);
        }
        public ActionResult Detail(string alias,int id)
        {
            var items = db.Products.Find(id);
            return View(items);
        }
        public ActionResult ProductCategoryMenuLeft(string alias,int id)
        {
            var items = db.Products.ToList();
            if (id > 0 )
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if(cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public  ActionResult  Partial_ItemsByCateid()
        {
            //var items = db.Products.Where(x=>x.IsHome && x.IsActive).Take(12).ToList(); chỉ lấy 12 sản phẩm
            var items = db.Products.Where(x => x.IsHome && x.IsActive).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult KhuyenMai(int? page)
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).ToList();
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(items.ToPagedList(pageNumber, pageSize));
    
   
        }
    }
}