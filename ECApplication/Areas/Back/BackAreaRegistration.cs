﻿using System.Web.Mvc;

namespace ECApplication.Areas.Back
{
    public class BackAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Back";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Back_default",
                "Back/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ECApplication.Areas.Back.Controllers" }
            );
        }
    }
}