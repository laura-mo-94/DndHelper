namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterAlignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Alignment", c => c.String());
            AddColumn("dbo.Characters", "GoodVals", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "EvilVals", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "ChaoticVals", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "LawfulVals", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "LawfulVals");
            DropColumn("dbo.Characters", "ChaoticVals");
            DropColumn("dbo.Characters", "EvilVals");
            DropColumn("dbo.Characters", "GoodVals");
            DropColumn("dbo.Characters", "Alignment");
        }
    }
}
