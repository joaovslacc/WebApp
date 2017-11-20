using Database;
using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (ConnectedId > 0)
                return RedirectToAction("Dashboard", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Connect(string username, string password)
        {
            try
            {
                using (var db = new Context())
                {
                    var accountRepository = new AccountRepository();
                    var account = await accountRepository.ConnectAsync(username, password);

                    if (account != null)
                        ConnectedId = account.Id;
                }

                return RedirectToAction("Dashboard", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                TransportData();
            }         
            return RedirectToAction("Index");
        }
    }
}