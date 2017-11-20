using Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class AccountRepository
    {
        public async Task<Account> ConnectAsync(string username, string password)
        {
            using (var db = new Context())
            {
                var account = await db.Accounts.Where(g => g.Username == username).FirstOrDefaultAsync();

                if (account == null || account.Password != password)
                    throw new Exception("O usuário ou senha inseridos não corresponde a nenhuma conta");

                return account;
            }
        }
    }
}
