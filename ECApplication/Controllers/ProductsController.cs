using ECApplication.Services;
using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ECApplication.Controllers
{
    public class ProductsController : ProductsBaseController
    {
        CommentService _service_Comment = new CommentService();
        OrderHeaderService _service_OrderHeader = new OrderHeaderService();

        public ActionResult ItemList(bool? activeFilter, int? categoryId, string sortBy, bool? desc, int pageNo = 1, string search = "")
        {
            var SelectItems = new List<SelectListItem>();
            SelectItems.Add(new SelectListItem() { Text = "下架", Value = "false" });
            SelectItems.Add(new SelectListItem() { Text = "上架", Value = "true" });
            ViewBag.ActiveFilter = new SelectList(SelectItems, "Value", "Text", (activeFilter.HasValue) ? activeFilter : true);
            ViewBag.ActiveFilterVal = activeFilter;

            ViewBag.Categorys = _service.GetCategory();  //new SelectList(_service.GetCategory(), "Id", "Name");
            ViewBag.PageNo = pageNo;
            ViewBag.sortBy = sortBy;
            ViewBag.desc = desc;

            IQueryable<Product> Products = _service.GetProduct(categoryId, sortBy, desc, activeFilter, search);

            return PartialView(Products.ToPagedList(pageNo, _pageSize));
        }

        public ActionResult Detail(int id)
        {
            ViewBag.AlreadyBuy = _service_OrderHeader.AlreadyBuy(id, User.Identity.Name);
            ViewBag.AlreadyCreate = _service_Comment.AlreadyCreate(id, User.Identity.Name);
            ViewBag.ProductId = id;
            return View(_service.GetProduct(id));
        }
    }
}