// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Concrete
{
    public class EfScheduleRepository : ScheduleRepository
    {
        EfDbContext context = new EfDbContext();

        public override IQueryable<Event> GetEvents() => context.Events;
        // TODO: public override IEvent[] GetObject(params int[] iventId)
        public override Event GetObject(int iventId)
        {
            var dbEntry = context.Events.Find(iventId);//FirstOrDefault
            return dbEntry;
        }

        public override IQueryable<EventsType> GetEventsTypes()
        {
            var dbEntry = context.EventsTypes;
            return dbEntry;
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
                    // TODU: Use method for copy http://docs.automapper.org
                    dbEntry.Access = _event.Access;
                    dbEntry.Accompanist = _event.Accompanist;
                    dbEntry.ActorsId = _event.ActorsId;
                    dbEntry.Choir = _event.Choir;
                    dbEntry.Choirmaster = _event.Choirmaster;
                    dbEntry.Conductor = _event.Conductor;
                    dbEntry.Description = _event.Description;
                    dbEntry.Duration = _event.Duration;
                    dbEntry.Id = _event.Id;
                    dbEntry.LightingDesigner = _event.LightingDesigner;
                    dbEntry.Location = _event.Location;
                    dbEntry.Mimic = _event.Mimic;
                    dbEntry.Orchestra = _event.Orchestra;
                    dbEntry.EventType = _event.EventType;
                    dbEntry.Producer = _event.Producer;
                    dbEntry.SoundEngineer = _event.SoundEngineer;
                    dbEntry.Start = _event.Start;
                    dbEntry.Title = _event.Title;

                    //dbEntry.Prefix = _event.Prefix;
                    //dbEntry = _event;
                    //Mapper.CreateMap<Event, Event>();
                    //Mapper.Map<Event, Event>(_event, dbEntry);
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
