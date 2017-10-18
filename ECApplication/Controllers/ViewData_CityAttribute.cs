using System;
using System.Web.Mvc;
using ECApplication.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ECApplication.Controllers
{
    public class ViewData_CityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TwCity TwCity;
            using (StreamReader file = new StreamReader(filterContext.HttpContext.Server.MapPath("~/App_Data/tw.json")))
            {
                TwCity = JsonConvert.DeserializeObject<TwCity>(file.ReadToEnd());
            };

            List<string> ListItems = new List<string>();
            foreach (var c in TwCity.taiwan)
            {
                foreach (var d in c.area)
                {
                    ListItems.Add(string.Format("{0} {1} {2}", d.zip, c.city, d.text));
                }
            }

            SelectList City = new SelectList(ListItems);
            filterContext.Controller.ViewData["ContactCity"] = City;
        }
    }
}