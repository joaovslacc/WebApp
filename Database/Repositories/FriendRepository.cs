using Database.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class FriendRepository
    {
        private Context context;
        public FriendRepository()
        {
            context = new Context();
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            var friends = await context.Friends.ToListAsync();
            return friends;
        }

        public async Task<Friend> GetByIdAsync(int id)
        {
            var friend = await context.Friends.FindAsync(id);
            return friend;
        }

        public async Task CreateOrEditAsync(Friend friend)
        {
            if (friend.Id == 0)
            {
                context.Friends.Add(friend);
            }
            else
            {
                var model = await GetByIdAsync(friend.Id);
                model.Name = friend.Name;
                model.Phone = friend.Phone;
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var friend = await GetByIdAsync(id);

            if (friend == null)
                throw new Exception("Amigo informado não encontrado");

            context.Friends.Remove(friend);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Friend>> FindMany(string search)
        {
            var friends = await context.Friends
                .Where(w => w.Name.Contains(search))
                .OrderBy(w => w.Name)
                .Take(5)
                .ToListAsync();

            return friends;
        }
    }
}
