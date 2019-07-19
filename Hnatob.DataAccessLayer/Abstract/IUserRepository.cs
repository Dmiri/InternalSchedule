using Hnatob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnatob.DataAccessLayer.Abstract
{
    public abstract class IUserRepository : IDisposable
    {
        public abstract IQueryable<Employee> GetEmployees();
        public abstract Employee GetEmployee(int Id);
        public abstract Employee GetEmployee(string Id);

        public abstract IQueryable<ApplicationUser> GetUsers();
        public abstract ApplicationUser GetUser(string Id);

        public abstract void Update(Employee person);
        public abstract ApplicationUser Delete(string personId);

        //===================================================================

        public abstract IQueryable<Position> GetPositions();

        //public abstract IEnumerable<Employee> GetEmployee();



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
        ~IUserRepository()
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
