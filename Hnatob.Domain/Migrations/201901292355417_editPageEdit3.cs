namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPageEdit3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Producer", c => c.String());
            AlterColumn("dbo.Events", "Conductor", c => c.String());
            AlterColumn("dbo.Events", "Choirmaster", c => c.String());
            AlterColumn("dbo.Events", "Accompanist", c => c.String());
            AlterColumn("dbo.Events", "LightingDesigner", c => c.String());
            AlterColumn("dbo.Events", "SoundEngineer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "SoundEngineer", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "LightingDesigner", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Accompanist", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Choirmaster", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Conductor", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Producer", c => c.Int(nullable: false));
        }
    }
}
