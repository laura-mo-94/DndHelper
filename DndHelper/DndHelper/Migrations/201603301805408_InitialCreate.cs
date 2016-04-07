namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignId = c.Int(nullable: false, identity: true),
                        CampaignName = c.String(nullable: false, maxLength: 100),
                        DungeonMasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignId);
            
            CreateTable(
                "dbo.CampaignsToPlayers",
                c => new
                    {
                        RelationId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        PlayerName = c.String(nullable: false, maxLength: 100),
                        CampaignId = c.Int(nullable: false),
                        CampaignName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RelationId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterID = c.Int(nullable: false, identity: true),
                        CharacterName = c.String(nullable: false, maxLength: 100),
                        Specie = c.String(nullable: false),
                        Class = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                        CurrentHealth = c.Int(nullable: false),
                        MaxHealth = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                        Alive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterID);
            
            CreateTable(
                "dbo.CharactersToPlayers",
                c => new
                    {
                        RelationId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        PlayerName = c.String(nullable: false, maxLength: 100),
                        CharacterId = c.Int(nullable: false),
                        CharacterName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RelationId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Players");
            DropTable("dbo.CharactersToPlayers");
            DropTable("dbo.Characters");
            DropTable("dbo.CampaignsToPlayers");
            DropTable("dbo.Campaigns");
        }
    }
}
