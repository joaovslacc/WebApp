using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Disconnect()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            using (var context = new Context())
            {
                //var account = context.Accounts.Where()
            }
            return View();
        }

        [RequireConnection]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}