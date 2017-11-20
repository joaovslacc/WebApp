namespace Database.Migrations
{
    using Database.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
            context.Accounts.AddOrUpdate(
               a => a.Username,
               new Account
               {
                   Username = "sysadmin",
                   Password = "102030",                  
               }
             );
        }
    }
}
