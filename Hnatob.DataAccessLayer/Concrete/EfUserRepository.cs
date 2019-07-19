using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.DataAccessLayer.Context;
using Hnatob.Domain.Models;


namespace Hnatob.DataAccessLayer.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public override IQueryable<Employee> GetEmployees() => context.Employee;
        public override Employee GetEmployee(int Id) => context.Employee.FirstOrDefault(p => p.Id == Id);
        public override Employee GetEmployee(string Id) => context.Employee.FirstOrDefault(p => p.UserId == Id);

        public override IQueryable<ApplicationUser> GetUsers() => context.Users;
        public override ApplicationUser GetUser(string Id) => context.Users.FirstOrDefault(p => p.Id == Id);

        // TODO: public override IEvent[] GetObject(params int[] iventId)
        public Employee GetObject(int iventId)
        {
            var dbEntry = context.Employee.Find(iventId);//FirstOrDefault
            if (dbEntry != null)
            {
                return dbEntry;
            }
            else
            {
                return null;
            }
        }
        
        public override void Update(Employee person)
        {

            //if (person == null) return;

            //if (person.Id == 0)
            //{
            //    context.People.Add(person);
            //}

            //else
            //{
            //    Employee dbEntry = context.People.Find(person.Id);
            //    if (dbEntry != null)
            //    {
            //        dbEntry = person;
            //    }
            //    else
            //    {
            //        //throw new Exception("Record didn't found");
            //    }
            //}
            //context.SaveChanges();
        }

        public override ApplicationUser Delete(string personId)
        {
            var dbEnry = context.Users.Find(personId);
            if (dbEnry != null)
            {
                context.Users.Remove(dbEnry);
                context.SaveChanges();
                return dbEnry;
            }
            else return null;
        }

        //===================================================================

        public override IQueryable<Position> GetPositions() => context.Positions;

        //public override IEnumerable<Employee> GetEmployee() => context.PositionPersons;
    }
}
