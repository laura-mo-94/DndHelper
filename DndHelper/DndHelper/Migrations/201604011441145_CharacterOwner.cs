namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "PlayerID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "PlayerID");
        }
    }
}
