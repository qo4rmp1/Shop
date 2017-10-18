using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using ECApplication.Models;

namespace ECApplication.Areas.Back.Controllers
{
    public class DdlController : Controller
    {
        // GET: Back/Ddl
        public ActionResult Index()
        {
            TwCity TwCity;

            using (StreamReader file = new StreamReader(Server.MapPath("~/App_Data/tw.json")))
            {
                TwCity = JsonConvert.DeserializeObject<TwCity>(file.ReadToEnd());
            };

            List<string> ListItems = new List<string>();
            foreach(var d in TwCity.taiwan)
            {
                ListItems.Add(d.city);
            }

            SelectList City = new SelectList(ListItems);
            ViewData["ContactCity"] = City;
            return View();
        }
    }
}