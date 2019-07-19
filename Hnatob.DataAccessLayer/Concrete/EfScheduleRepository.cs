// System
using System;
using System.Collections.Generic;
using System.Linq;

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.DataAccessLayer.Context;
using Hnatob.Domain.Models;

namespace Hnatob.DataAccessLayer.Concrete
{
    public class EfScheduleRepository : IScheduleRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public override IQueryable<Event> GetEvents() => context.Events;

        public override Event GetObject(int iventId)
        {
            var dbEntry = context.Events.Find(iventId);//FirstOrDefault
            return dbEntry;
        }


        public override void Update(Event _event)
        {

            if (_event == null) return;
            Event dbEntry;
            if (_event.Id == 0)
                dbEntry = new Event();

            else dbEntry = context.Events.FirstOrDefault(e => e.Id == _event.Id);
            if (dbEntry != null)
            {
                // TODU: Use method for copy http://docs.automapper.org
                dbEntry.Access = _event.Access;
                dbEntry.Description = _event.Description;
                dbEntry.Duration = _event.Duration;
                dbEntry.Id = _event.Id;
                dbEntry.Location = _event.Location;
                dbEntry.EventType = _event.EventType;
                dbEntry.Start = _event.Start;
                dbEntry.Title = _event.Title;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var responsibles = dbEntry.Responsibles.ToList();
                        foreach (var item in responsibles)
                        {
                            context.Responsibles.Remove(item);
                        }
                        dbEntry.Responsibles.AddRange(_event.Responsibles);

                        var service = dbEntry.CommentsToServices.ToList();
                        foreach (var item in service)
                        {
                            context.CommentsToservices.Remove(item);
                        }
                        dbEntry.CommentsToServices.AddRange(_event.CommentsToServices);
                        if (_event.Id == 0) context.Events.Add(dbEntry);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception("Record failed: responsibles / services - " + e.Message);
                    }
                }
            }
            else
            {
                throw new Exception("Record failed: this object does not exist.");
            }


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
