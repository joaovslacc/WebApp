using Database.Models;
using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FriendController : Controller
    {
        private FriendRepository friendRepository;
        public FriendController()
        {
            friendRepository = new FriendRepository();
        }

        [RequireConnection]
        public async Task<ActionResult> Index()
        {
            var friends = await friendRepository.GetAllAsync();

            return View(friends);
        }

        [RequireConnection]
        public async Task<ActionResult> Edit(int id = 0)
        {
            var friend = await friendRepository.GetByIdAsync(id);
            friend = friend ?? new Friend();
            return View(friend);
        }

        [HttpPost]
        [RequireConnection]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Friend friend)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await friendRepository.CreateOrEditAsync(friend);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                } 
            }

            return View(friend);
        }

        public async Task<ActionResult> Delete(int id = 0)
        {
            if (id != 0)
            {
                var friend = await friendRepository.GetByIdAsync(id);

                if (friend == null)
                    return HttpNotFound();

                return View(friend); 
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            try
            {
                await friendRepository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                TransportData();
            }

            return RedirectToAction("Index");

        }

        [RequireConnection]
        public async Task<ActionResult> FindMany(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var friends = await friendRepository.FindMany(search);
            return Json(friends, JsonRequestBehavior.AllowGet);
        }
    }
}