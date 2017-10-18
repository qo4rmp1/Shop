using ECApplication.Models;
using ECApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECApplication.Controllers
{
    public class CartController : Controller
    {
        ProductsService _service_Products = new ProductsService();
        //CartService _service_Cart = new CartService();

        protected List<Cart> Carts
        {
            get
            {
                if (Session["Carts"] == null)
                {
                    Session["Carts"] = new List<Cart>();
                }
                return (Session["Carts"] as List<Cart>);
            }
            set
            {
                Session["Carts"] = value;
            }
        }

        // 加入商品到購物車
        [HttpPost]
        public ActionResult AddToCart(int productId, int amount = 1)
        {
            Product Product = _service_Products.GetProduct(productId);
            if (Product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Cart ExistCart = this.Carts.Where(p => p.ProductId == productId).FirstOrDefault();
            if (ExistCart == null)
            {
                this.Carts.Add(new Cart() { ProductId = productId, Amount = amount, Product = Product });
            }
            else
            {
                ExistCart.Amount++;
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        // 顯示目前購物車項目
        public ActionResult Index(bool Enable = true)
        {
            ViewBag.Enable = Enable;
            return View(this.Carts);
        }

        // 更新購物車內特定項目的購物數量
        [HttpPost]
        public ActionResult UpdateAmount(List<Cart> carts)
        {
            if (this.Carts.Count > 0)
            {
                foreach (var item in carts)
                {
                    var ExistCart = this.Carts.Where(p => p.ProductId == item.ProductId).FirstOrDefault();

                    if (ExistCart != null)
                    {
                        ExistCart.Amount = item.Amount;
                    }
                }
            }

            return RedirectToAction("Complete", "Order");
        }

        // 刪除購物車內商品
        [HttpPost]
        public ActionResult Remove(int productId)
        {
            Cart Cart = this.Carts.Where(p => p.ProductId == productId).FirstOrDefault();
            if (Cart == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.Carts.Remove(Cart);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}