using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public class RequireConnectionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["ConnectedId"] == null)
            {
                filterContext.HttpContext.Session.Abandon();
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" } });
            }
            else
            {
                var id = (int?)filterContext.HttpContext.Session["ConnectedId"];

                using (var context = new Context())
                {
                    var account = context.Accounts.Where(w => w.Id == id).FirstOrDefault();

                    if (account == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" } });
                    }
                }

            }
        }
    }
}