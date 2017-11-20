using Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Database.Repositories
{

    public class GameRepository
    {
        private Context context;
        public GameRepository()
        {
            context = new Context();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var game = await context.Games.FindAsync(id);
            return game;
        }

        public async Task CreateOrEditAsync(Game game)
        {
            if (game.Id == 0)
            {
                game.Available = true;
                context.Games.Add(game);
            }
            else
            {
                var model = await context.Games.FindAsync(game.Id);
                model.Name = game.Name;
            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> FindMany(string search)
        {
            var games = await context.Games
                .Where(w => w.Name.Contains(search))
                .OrderBy(w => w.Name)
                .Take(5)
                .ToListAsync();

            return games;
        }

        public async Task LendingAsync(int gameId, int friendId, int days)
        {
            var game = await GetByIdAsync(gameId);

            game.Available = false;
            game.FriendId = friendId;
            game.LendingDate = DateTime.Now;
            game.Deadline = DateTime.Now.AddDays(Convert.ToDouble(days));

            await context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var game = await GetByIdAsync(id);

            if (game == null)
                throw new Exception("O jogo informado não existe");

            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }

        public async Task Devolution(int id)
        {
            var game = await GetByIdAsync(id);
            if (game == null)
                throw new Exception("O jogo informado não existe");

            game.Available = true;
            game.Deadline = null;
            game.LendingDate = null;
            game.FriendId = null;
            await context.SaveChangesAsync();
        }
    }
}
