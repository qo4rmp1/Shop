using ECApplication.Models;
using ECApplication.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ECApplication.Controllers
{
    public abstract class ProductsBaseController : Controller
    {
        public ProductsService _service;
        public int _pageSize = 6;

        public ProductsBaseController()
        {
            _service = new ProductsService();
        }

        public string GetImageUrl(Product product)
        {
            UrlHelper UrlHelper = new UrlHelper(Request.RequestContext);
            return UrlHelper.Content("~/images/" + product.FileName);
        }

        public ActionResult GetImage(int id)
        {
            Product Product = _service.GetProduct(id);

            if (Product != null)
            {
                // Way 1
                //return File(Product.PhotoFile, Product.ImageMimeType);
                // Way 2
                // 使用UrlHelper產生圖檔路徑
                string Url = GetImageUrl(Product);
                // 回傳圖片路徑
                return File(Url, Product.ImageMimeType);
                //return Content(UrlHelper.Content("~/images/" + Product.FileName));
                //return base.File(UrlHelper.Content("~/images/" + Product.FileName), Product.Type);
                //Response.ContentType = Product.ImageMimeType;
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }


        public ActionResult Index(bool? activeFilter, int? categoryId, int pageNo = 1, string search = "")
        {
            // 實際上只有Back才會用到
            var SelectItems = new List<SelectListItem>();
            SelectItems.Add(new SelectListItem() { Text = "下架", Value = "false" });
            SelectItems.Add(new SelectListItem() { Text = "上架", Value = "true" });
            ViewBag.ActiveFilter = new SelectList(SelectItems, "Value", "Text", (activeFilter.HasValue) ? activeFilter : null);
            ViewBag.ActiveFilterVal = activeFilter;

            //ViewBag.Categorys = _service.GetCategory();  //new SelectList(_service.GetCategory(), "Id", "Name");
            //ViewBag.PageNo = pageNo;
            //IQueryable<Product> Products = _service.GetProduct(categoryId, activeFilter, search);

            //return View(Products.ToPagedList(pageNo, _pageSize));
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainDropdown()
        {
            return PartialView(_service.GetCategory());
        }
    }
}