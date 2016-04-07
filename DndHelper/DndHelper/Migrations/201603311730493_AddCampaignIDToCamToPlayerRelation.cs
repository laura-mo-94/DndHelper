namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampaignIDToCamToPlayerRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CampaignsToPlayers", "CharacterID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CampaignsToPlayers", "CharacterID");
        }
    }
}
