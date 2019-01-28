// System
using System.Data.Entity;

// Project
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("DefaultConnection") { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> PositionPersons { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<EPosition> EPositions { get; set; }
    }
}
