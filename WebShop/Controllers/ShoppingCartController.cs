using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.Models.EF;

namespace WebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
           
            return View();
        }
        [Authorize]

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
              ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
         
           
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel rq)
        {
            var code = new { Success = false, code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    Order od = new Order();
                    od.CustomerName = rq.CustomerName;
                    od.Phone = rq.Phone;
                    od.Address = rq.Address;
                    cart.Items.ForEach(x => od.orderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,

                    }));
                    od.TotalAmount = cart.Items.Sum(x=>(x.Quantity * x.Price));
                    od.TypePayment = rq.TypePayment;
                    od.CreatedDate = DateTime.Now;
                    od.ModifiedDate = DateTime.Now;
                    od.CreatedBy = rq.Phone;
                    od.Email = rq.Email;
                    Random rd = new Random();
                    od.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(od);
                    db.SaveChanges();
                    //send email khach hang
                    var strSanPham = "";
                    var thanhTien = decimal.Zero;
                    var tongTien = decimal.Zero;
                    foreach(var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+sp.ProductName+"</td>";
                        strSanPham += "<td>"+sp.Quantity+"</td>";
                        strSanPham += "<td>"+WebShop.Models.Commons.Format.FormatNumber(sp.Price,0)+"</td>";
                        strSanPham += "</tr>";
                        thanhTien += sp.Price * sp.Quantity;
                    }
                    tongTien = thanhTien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_sendEmail/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", od.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", od.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", od.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", od.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiGiaoHang}}", od.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WebShop.Models.Commons.Format.FormatNumber(thanhTien, 0) );
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WebShop.Models.Commons.Format.FormatNumber(tongTien, 0));
                    WebShop.Models.Commons.Format.SendMail("NTVFIGUREShop", "Đơn hàng #" + od.Code, contentCustomer, od.Email);

                    // admin 
                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_sendEmail/send1.html"));
                   contentAdmin = contentAdmin.Replace("{{MaDon}}", od.Code);
                   contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                   contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                   contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", od.CustomerName);
                   contentAdmin = contentAdmin.Replace("{{Phone}}", od.Phone);
                   contentAdmin = contentAdmin.Replace("{{Email}}", od.Email);
                   contentAdmin = contentAdmin.Replace("{{DiaChiGiaoHang}}", od.Address);
                   contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WebShop.Models.Commons.Format.FormatNumber(thanhTien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WebShop.Models.Commons.Format.FormatNumber(tongTien, 0));
                    WebShop.Models.Commons.Format.SendMail("NTVFIGUREShop", "Đơn hàng mới #" + od.Code, contentAdmin, ConfigurationManager.AppSettings["EmailAdmin"]);


                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(code);
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCount() 
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new {  Count = cart.Items.Count },JsonRequestBehavior.AllowGet);

            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id,int quatity) 
        {
            var code = new { Success = false, msg = "", code = -1 ,Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if(checkProduct != null) { 
                ShoppingCart cart =(ShoppingCart)Session["Cart"];
                if(cart == null) {
                
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Quantity = quatity,
                    Alias = checkProduct.Alias,

                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImage = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quatity);
                Session["Cart"] = cart;
                code = new { Success = true, msg ="Thêm sản phẩm vào giỏ hàng thành công!", code = 1 , Count = cart.Items.Count};
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quatity)
        {
            var item = db.Products.Find(id);
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                if(item.Quantity > quatity)
                {
                    cart.UpdateQuantity(id, quatity);
                    return Json(new { Success = true });
                }
                else
                {
                    return Json(new { Success = false });
                }
             

            }
            return Json(new { Success = false });

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct =cart.Items.FirstOrDefault(x =>x.ProductId == id);
                if(checkProduct != null) {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);

        }
    
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                cart.ClearCart();
                return Json(new {Success = true});

            }
            return Json(new { Success = false });

        }
    }
}