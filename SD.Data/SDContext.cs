using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data
{
    public partial class SDContext : IDisposable
    {
        private bool disposed = false;
        private SmartEntities sdContext = new SmartEntities();
        public SmartEntities SmartContext
        {
            get { return sdContext; }
        }

        public SDContext()
        {
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            try
            {
                if (sdContext != null && sdContext.ChangeTracker != null && sdContext.ChangeTracker.HasChanges())
                {
                    sdContext.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex1)
            {
                throw ex1;
            }
            catch (DbUpdateException ex2)
            {
                throw ex2;
            }
            catch (SqlException ex3)
            {
                throw ex3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public async Task SaveAsync()
        {
            try
            {
                if (sdContext != null && sdContext.ChangeTracker != null && sdContext.ChangeTracker.HasChanges())
                {
                    await sdContext.SaveChangesAsync();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex1)
            {
                throw ex1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"/>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    sdContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
