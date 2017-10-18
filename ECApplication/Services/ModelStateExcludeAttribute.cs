using System;
using System.Web.Mvc;

namespace ECApplication.Services
{
    public class ModelStateExcludeAttribute : ActionFilterAttribute
    {
        public string[] Exclude { get; set; }

        public ModelStateExcludeAttribute(string exclude)
        {
            if (string.IsNullOrEmpty(exclude) == false)
            {
                if (exclude.Contains(","))
                {
                    Exclude = exclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Exclude = new string[] { exclude };
                }
            }
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var item in Exclude)
            {
                filterContext.Controller.ViewData.ModelState.Remove(item);
            }

            filterContext.Controller.ViewData["Exclude"] = Exclude;
        }
    }
}