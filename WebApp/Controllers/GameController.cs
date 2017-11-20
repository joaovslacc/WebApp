using Database;
using Database.Models;
using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private GameRepository gameRepository;
        public GameController()
        {
            gameRepository = new GameRepository();
        }

        [RequireConnection]
        public async Task<ActionResult> Index(string search, int page = 1)
        {
            List<Game> games = new List<Game>();

            try
            {
                using (var context = new Context())
                {
                    int skip = (page - 1) * ItemsPerPage;

                    ViewBag.Search = search;

                    games = await context.Games.Where(w => (string.IsNullOrEmpty(search) ? true : w.Name.Contains(search)))
                        .Include(i => i.Friend)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(games);
        }

        [RequireConnection]

        public async Task<ActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                var game = new Game()
                {
                    Available = true
                };
                return View(game);
            }
            else
            {
                var game = await gameRepository.GetByIdAsync(id);

                if (game != null)
                {
                    return View(game);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await gameRepository.CreateOrEditAsync(game);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(game);
        }

        [RequireConnection]
        public ActionResult Lending()
        {
            var viewModel = new LendingViewModel()
            {
                Game = new Game(),
                Friend = new Friend()
            };

            return View(viewModel);
        }

        [HttpPost]
        [RequireConnection]
        public async Task<ActionResult> Lending(int game = 0 , int friend = 0, int days = 0)
        {            
            try
            {
                if (game == 0 || friend == 0 || days == 0)
                {
                    throw new Exception("Selecione o Amigo e o Jogo que deseja emprestar. Não se esqueça de colocar o prazo para devolução.");
                }

                await gameRepository.LendingAsync(game, friend, days);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var viewModel = new LendingViewModel()
                {
                    Game = new Game(),
                    Friend = new Friend()
                };
                return View(viewModel);

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

            var games = await gameRepository.FindMany(search);
            games = games.Where(w => w.Available);

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [RequireConnection]
        public async Task<ActionResult> Delete(int id = 0)
        {
            if (id != 0)
            {
                var game = await gameRepository.GetByIdAsync(id);

                if (game == null)
                    return HttpNotFound();

                return View(game);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [RequireConnection]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            try
            {
                await gameRepository.Remove(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                TransportData();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [RequireConnection]
        public async Task<ActionResult> Devolution(int id = 0)
        {
            try
            {
                await gameRepository.Devolution(id);
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