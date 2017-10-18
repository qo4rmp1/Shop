using ECApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECApplication.Models;
using ECApplication.Models.ViewModels;
using System.IO;
using Newtonsoft.Json;

namespace ECApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        MemberService _service_Member = new MemberService();
        OrderHeaderService _service_OrderHeader = new OrderHeaderService();
        OrderDetailService _service_OrderDetail = new OrderDetailService();
        ProductsService _service_Product = new ProductsService();

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

        // 顯示訂單
        [ViewData_City]
        public ActionResult Complete()
        {
            Member Member = _service_Member.GetMember(User.Identity.Name);

            if (Member == null)
            {
                return RedirectToAction("Login", "Member");
            }

            if (this.Carts.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            // 取出OrderHeader
            OrderHeader ExistOH = _service_OrderHeader.GetOrderHeaderByMember(Member.UserName).FirstOrDefault();

            // 儲存OrderDetail
            if (ExistOH != null)
            {
                ExistOH.Memo = "";
                ViewData.Model = ExistOH;
            }

            return View();
        }
        
        // 送出訂單，儲存到資料庫
        [ViewData_City]
        [HttpPost]
        public ActionResult Complete(OrderHeader form)
        {
            Member Member = _service_Member.GetMember(User.Identity.Name);
            if (Member == null)
            {
                return RedirectToAction("Login", "Member");
            }

            if (this.Carts.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            // 計算總金額
            int TotalPrice = 0;
            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            foreach (var item in this.Carts)
            {
                var Product = _service_Product.GetProduct(item.Product.ProductId);

                if (Product != null)
                {
                    // 計算總金額
                    TotalPrice += (int)item.Product.Price * item.Amount;

                    // 將OrderDetail加入序列
                    OrderDetails.Add(new OrderDetail()
                    {
                        ProductId = Product.ProductId,
                        Price = Product.Price,
                        Amount = item.Amount
                    });
                }
            }


            // TODO: 將訂單寫入資料庫
            // 儲存OrderHeader
            OrderHeader OrderHeader = new OrderHeader()
            {
                UserName = Member.UserName,
                ContactName = form.ContactName,
                ContactCity = form.ContactCity,
                ContactStreet1 = form.ContactStreet1,
                ContactPhoneNo = form.ContactPhoneNo,
                BuyOn = DateTime.Now,
                Memo = form.Memo
            };

            // 儲存OrderHeader
            OrderHeader.TotalPrice = TotalPrice;
            _service_OrderHeader.Add(OrderHeader);
            _service_OrderHeader.Save();

            // 儲存OrderDetail
            // 取出OrderHeader
            OrderHeader SaveOH = _service_OrderHeader.GetOrderHeaderByMember(Member.UserName).FirstOrDefault();

            if (SaveOH != null)
            {
                foreach (var item in OrderDetails)
                {
                    item.OrderHeaderId = SaveOH.Id;
                    _service_OrderDetail.Add(item);
                }
                _service_OrderDetail.Save();
            }

            // TODO: 清空購物車資料
            this.Carts.Clear();

            return RedirectToAction("Index", "Products");
        }

        public ActionResult Histories()
        {
            Member Member = _service_Member.GetMember(User.Identity.Name);
            if (Member == null)
            {
                return RedirectToAction("Login", "Member");
            }

            List<OrderHeader> OrderHeaders = _service_OrderHeader.GetOrderHeaderByMember(Member.UserName);
            
            return View(OrderHeaders);
        }

        public ActionResult History(int headerId)
        {
            OrderHistoryVM OhVM = new OrderHistoryVM();

            OhVM.Oh = _service_OrderHeader.GetOrderHeader(headerId);

            if (OhVM.Oh != null)
            {
                OhVM.Ods = _service_OrderDetail.GetOrderDetailByHeader(OhVM.Oh.Id);
            }

            return PartialView(OhVM);
        }
    }
}