namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMtoM2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PositionPersons", newName: "PositionPersons");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PositionPersons", newName: "PositionPersons");
        }
    }
}
