using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Concrete
{
    class EfPeopleRepository : IPeopleRepository
    {
        EfDbContext context = new EfDbContext();

        public IEnumerable<IPerson> People => context.People;

        public IPerson DeletePerson(int personId)
        {
            var dbEnry = context.People.Find(personId);
            if (dbEnry != null)
            {
                context.People.Remove(dbEnry);
                context.SaveChanges();
            }
            return dbEnry;
        }

        public void SavePerson(IPerson person)
        {
            if (person == null) return;

            if (person.Id == 0)
            {
                //context.People.Add(person);
            }

            else
            {
                var dbEntry = context.People.Find(person.Id);
                if (dbEntry != null)
                {
                    //dbEntry = person;
                }
                else
                {
                    //throw new Exception("Person didn't found");
                }
            }
            context.SaveChanges();
        }
    }
}
