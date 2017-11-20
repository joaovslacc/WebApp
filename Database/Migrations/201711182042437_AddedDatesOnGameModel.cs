namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDatesOnGameModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "LendingDate", c => c.DateTime());
            AddColumn("dbo.Games", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Deadline");
            DropColumn("dbo.Games", "LendingDate");
        }
    }
}
