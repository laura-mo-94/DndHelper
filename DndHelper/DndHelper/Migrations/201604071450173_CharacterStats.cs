namespace DndHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterStats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "BaseStrength", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "BaseDexterity", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "BaseConstitution", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "BaseIntelligence", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "BaseWisdom", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "BaseCharisma", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "StrengthModifier", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "DexterityModifier", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "ConstitutionModifier", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "IntelligenceModifier", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "WisdomModifier", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "CharismaModifier", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "CharismaModifier");
            DropColumn("dbo.Characters", "WisdomModifier");
            DropColumn("dbo.Characters", "IntelligenceModifier");
            DropColumn("dbo.Characters", "ConstitutionModifier");
            DropColumn("dbo.Characters", "DexterityModifier");
            DropColumn("dbo.Characters", "StrengthModifier");
            DropColumn("dbo.Characters", "BaseCharisma");
            DropColumn("dbo.Characters", "BaseWisdom");
            DropColumn("dbo.Characters", "BaseIntelligence");
            DropColumn("dbo.Characters", "BaseConstitution");
            DropColumn("dbo.Characters", "BaseDexterity");
            DropColumn("dbo.Characters", "BaseStrength");
        }
    }
}
