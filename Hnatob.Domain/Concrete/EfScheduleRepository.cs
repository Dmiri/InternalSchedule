// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Concrete
{
    public class EfScheduleRepository : ScheduleRepository
    {
        EfDbContext context = new EfDbContext();

        public override IEnumerable<Event> GetList() => context.Events;

        // TODO: public override IEvent[] GetObject(params int[] iventId)
        public override Event GetObject(int iventId)
        {
            var dbEntry = context.Events.Find(iventId);//FirstOrDefault
            if (dbEntry != null)
            {
                return dbEntry;
            }
            else
            {
                return null;
            }
        }

        public override void Update(Event _event)
        {

            if (_event == null) return;

            if (_event.Id == 0)
            {
                context.Events.Add(_event);
            }

            else
            {
                Event dbEntry = context.Events.Find(_event.Id);
                if (dbEntry != null)
                {
                    //dbEntry.Prefix = _event.Prefix;
                    dbEntry = _event;
                }
                else
                {
                    //throw new Exception("Record didn't found");
                }
            }
            context.SaveChanges();

        }

        public override Event Delete(int eventId)
        {
            var dbEnry = context.Events.Find(eventId);
            if(dbEnry != null)
            {
                context.Events.Remove(dbEnry);
                context.SaveChanges();
            }
            return dbEnry;
        }



        #region IDisposable Support
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~EfScheduleRepository() {
           // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
           Dispose(false);
        }

        #endregion

    }
}
