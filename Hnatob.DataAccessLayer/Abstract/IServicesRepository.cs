﻿// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Project
using Hnatob.Domain.Models;

namespace Hnatob.DataAccessLayer.Abstract
{
    public abstract class ICommentToServiceRepository : IDisposable
    {
        public abstract IQueryable<CommentToService> GetCommentToServices();

        public abstract IQueryable<CommentToService> GetCommentToServices(int EventId);

        public abstract void UpdateCommentToService(CommentToService model);

        public abstract void UpdateCommentToService(IEnumerable<CommentToService> model);


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
        ~ICommentToServiceRepository()
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