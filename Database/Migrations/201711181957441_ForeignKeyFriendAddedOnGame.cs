namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyFriendAddedOnGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "FriendId", c => c.Int());
            CreateIndex("dbo.Games", "FriendId");
            AddForeignKey("dbo.Games", "FriendId", "dbo.Friends", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "FriendId", "dbo.Friends");
            DropIndex("dbo.Games", new[] { "FriendId" });
            DropColumn("dbo.Games", "FriendId");
        }
    }
}
