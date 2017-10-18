using ECApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECApplication.Controllers
{
    public class HomeController : Controller
    {
        ProductsService _service_Product;

        public HomeController()
        {
            _service_Product = new ProductsService();
        }

        public ActionResult Index()
        {
            return View(_service_Product.GetProduct(null, null, null, null));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}