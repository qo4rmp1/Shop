using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security;
using System.Web.Security;
using System.Security.Principal;

namespace ECApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            IPrincipal contextUser = Context.User;

            if (contextUser.Identity.AuthenticationType == "Forms")
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                string[] roles = ticket.UserData.Split(new char[] { ',' });
                HttpContext.Current.User = new GenericPrincipal(User.Identity, roles);
                //Thread.CurrentPrincipal = HttpContext.Current.User;
            }
        }

        //protected void Application_AuthenticationRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.User != null)
        //    {
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            if (HttpContext.Current.User.Identity is FormsIdentity)
        //            {
        //                FormsIdentity Id = (FormsIdentity)HttpContext.Current.User.Identity;
        //                FormsAuthenticationTicket Ticket = Id.Ticket;

        //                string UserData = Ticket.UserData;
        //                string[] Roles = UserData.Split(',');
        //                HttpContext.Current.User = new GenericPrincipal(Id, Roles);
        //            }
        //        }
        //    }
        //}
    }
}
