namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampaignDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campaigns", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Campaigns", "Description");
        }
    }
}
