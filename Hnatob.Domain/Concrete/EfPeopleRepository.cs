using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;


namespace Hnatob.Domain.Concrete
{
    public class EfPeopleRepository : IPeopleRepository
    {
        EfDbContext context = new EfDbContext();

        public override IEnumerable<Person> GetPeople() => context.People;

        // TODO: public override IEvent[] GetObject(params int[] iventId)
        public Person GetObject(int iventId)
        {
            var dbEntry = context.People.Find(iventId);//FirstOrDefault
            if (dbEntry != null)
            {
                return dbEntry;
            }
            else
            {
                return null;
            }
        }

        public override void Update(Person person)
        {

            if (person == null) return;

            if (person.Id == 0)
            {
                context.People.Add(person);
            }

            else
            {
                Person dbEntry = context.People.Find(person.Id);
                if (dbEntry != null)
                {
                    dbEntry = person;
                }
                else
                {
                    //throw new Exception("Record didn't found");
                }
            }
            context.SaveChanges();
        }

        public override Person Delete(int personId)
        {
            var dbEnry = context.People.Find(personId);
            if (dbEnry != null)
            {
                context.People.Remove(dbEnry);
                context.SaveChanges();
            }
            return dbEnry;
        }

        //===================================================================

        public override IEnumerable<Position> GetPositions() => context.Positions;

        public override IEnumerable<Employee> GetEmployee() => context.PositionPersons;


    }
}
