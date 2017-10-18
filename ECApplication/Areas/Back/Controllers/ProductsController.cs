using ECApplication.Services;
using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using ECApplication.Controllers;
using PagedList;

namespace ECApplication.Areas.Back.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductsController : ProductsBaseController
    {
        public ActionResult Batch(bool? activeFilter, int? categoryId, string sortBy, bool? desc, int pageNo = 1, string search = "")
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

        [HttpPost]
        public ActionResult Batch(Product[] data, bool? activeFilter, int? categoryId, string sortBy, bool? desc, int pageNo = 1, string search = "")
        {
            Product product = null;
            foreach (var item in data)
            {
                product = _service.GetProduct(item.ProductId);

                if (product != null)
                {
                    product.ProductName = item.ProductName;
                    product.Price = item.Price;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                }
            }

            _service.Save();

            var SelectItems = new List<SelectListItem>();
            SelectItems.Add(new SelectListItem() { Text = "下架", Value = "false" });
            SelectItems.Add(new SelectListItem() { Text = "上架", Value = "true" });
            ViewBag.ActiveFilter = new SelectList(SelectItems, "Value", "Text", (activeFilter.HasValue) ? activeFilter : true);
            ViewBag.ActiveFilterVal = activeFilter;

            ViewBag.Categorys = _service.GetCategory();  //new SelectList(_service.GetCategory(), "Id", "Name");
            ViewBag.PageNo = pageNo;

            IQueryable<Product> Products = _service.GetProduct(categoryId, sortBy, desc, activeFilter, search);
            return PartialView(Products.ToPagedList(pageNo, _pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.Categorys = _service.GetCategory();
            //ViewBag.Categorys = new SelectList(_service.GetCategory(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        //[ModelStateExclude("PhotoFile,ImageMimeType")]
        public ActionResult Create([Bind(Exclude = "PhotoFile,ImageMimeType,FileName,Size,Url")]Product product, HttpPostedFileBase image)  //FormCollection collection
        {
            ViewBag.Categorys = _service.GetCategory();
            //ViewBag.Categorys = new SelectList(_service.GetCategory(), "Id", "Name");

            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        string FileName = image.FileName;
                        string Url = Path.Combine(Server.MapPath("~/images/"), FileName);
                        image.SaveAs(Url);

                        product.FileName = FileName;
                        product.Url = Server.MapPath("~/images/") + FileName;
                        product.Size = image.ContentLength;

                        byte[] PhotoFile = new byte[image.ContentLength];
                        image.InputStream.Read(PhotoFile, 0, image.ContentLength);

                        product.PhotoFile = PhotoFile;
                        product.ImageMimeType = image.ContentType;
                    }

                    _service.Add(product);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "模型繫結發生錯誤");
                    return View(product);
                }
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Back/Products/Edit/5
        public ActionResult Edit(int id)
        {
            Product Product = _service.GetProduct(id);
            ViewBag.Categorys = _service.GetCategory();
            //ViewBag.Categorys = new SelectList(_service.GetCategory(), "Id", "Name");
            return View(Product);
        }

        // POST: Back/Products/Edit/5
        [HttpPost]
        [ModelStateExclude("PhotoFile,ImageMimeType,FileName,Size,Url")]
        public ActionResult Edit(int id, [Bind(Exclude = "PhotoFile,ImageMimeType,FileName,Size,Url")]Product product)  //FormCollection collection
        {
            try
            {
                Product Product = _service.GetProduct(id);

                if (Product == null)
                {
                    return HttpNotFound();
                }

                if (TryUpdateModel<Product>(Product, "", new string[] { "ProductName", "Price", "Active", "Stock", "Description", "CategoryId" }, ViewData["Exclude"] as string[]))
                {
                    _service.Save();
                }
                else
                {
                    ModelState.AddModelError("", "模型繫結發生錯誤");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Categorys = _service.GetCategory();
                //ViewBag.Categorys = new SelectList(_service.GetCategory(), "Id", "Name");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product Product = _service.GetProduct(id);
                _service.Delete(Product);
                // TODO: Add delete logic here

                return new HttpStatusCodeResult(HttpStatusCode.OK);
                //return JavaScript("alert('商品已刪除');");
                //return RedirectToAction("Index");
            }
            catch (Exception except)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                //return JavaScript("alert('商品刪除發生錯誤');");
                //return View();
            }
        }

        [HttpPost]
        public string EditImage(int id)  //, HttpPostedFileBase image
        {
            if (Request.Files.Count > 0)
            {
                var image = Request.Files[0];

                Product Product = _service.GetProduct(id);

                if (image != null && Product != null)
                {
                    string FileName = image.FileName;
                    string Url = Path.Combine(Server.MapPath("~/images/"), FileName);
                    image.SaveAs(Url);

                    Product.FileName = FileName;
                    Product.Url = Server.MapPath("~/images/") + FileName;
                    Product.Size = image.ContentLength;

                    byte[] PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(PhotoFile, 0, image.ContentLength);

                    Product.PhotoFile = PhotoFile;
                    Product.ImageMimeType = image.ContentType;

                    _service.Save();
                    
                    // 回傳圖片路徑
                    Response.ContentType = Product.ImageMimeType;
                    return GetImageUrl(Product);
                }

                return "";
            }
            return "";
        }

    }
}
