using System;
using System.Threading.Tasks;
using Clean.DataAccess.EntityFramework;

namespace Clean.DataAccess.UnitOfWork.Base.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : CleanDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
