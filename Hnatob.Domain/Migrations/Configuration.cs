namespace Hnatob.Domain.Migrations
{
    using Hnatob.Domain.Helper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hnatob.Domain.Concrete.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Hnatob.Domain.Concrete.EfDbContext";
        }

        protected override void Seed(Hnatob.Domain.Concrete.EfDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
