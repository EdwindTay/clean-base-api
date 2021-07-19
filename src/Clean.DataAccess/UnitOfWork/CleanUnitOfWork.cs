using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.UnitOfWork.Base;

namespace Clean.DataAccess.UnitOfWork
{
    public class CleanUnitOfWork : UnitOfWork<CleanDbContext>
    {
        public CleanUnitOfWork(CleanDbContext context) : base(context)
        {
        }
    }
}
