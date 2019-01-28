namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(),
                        Producer = c.Int(nullable: false),
                        Conductor = c.Int(nullable: false),
                        Choirmaster = c.Int(nullable: false),
                        Accompanist = c.Int(nullable: false),
                        LightingDesigner = c.Int(nullable: false),
                        SoundEngineer = c.Int(nullable: false),
                        Orchestra = c.Boolean(nullable: false),
                        Choir = c.Boolean(nullable: false),
                        Mimic = c.Boolean(nullable: false),
                        Access = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Duration = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                        Introduction = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Patronymic = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Positions");
            DropTable("dbo.People");
            DropTable("dbo.Events");
        }
    }
}
