﻿// System
using System;
using System.Collections.Generic;

// Project
using Hnatob.Domain.Concrete;

namespace Hnatob.Domain.Abstract
{
    public abstract class ScheduleRepository : IDisposable
    {
        //EfDbContext context = new EfDbContext();

        public abstract IEnumerable<Event> GetList();

        public abstract Event GetObject(int iventId);

        public abstract void Update(Event model);

        public abstract Event Delete(int eventId);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~ScheduleRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
