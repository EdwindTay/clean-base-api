using System;
using System.Threading.Tasks;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.UnitOfWork.Base.Interfaces;

namespace Clean.DataAccess.UnitOfWork.Base
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : CleanDbContext
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        protected T dbContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(T context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int SaveChanges()
        {
            // Save changes with the default options
            return dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves all pending changes asynchronously
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public async Task<int> SaveChangesAsync()
        {
            // Save changes with the default options
            return await dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
}
