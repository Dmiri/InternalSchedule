namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IEvents", newName: "Events");
            RenameTable(name: "dbo.IPersons", newName: "People");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.People", newName: "IPersons");
            RenameTable(name: "dbo.Events", newName: "IEvents");
        }
    }
}
