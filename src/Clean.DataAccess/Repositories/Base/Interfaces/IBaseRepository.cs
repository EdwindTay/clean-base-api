using System.Linq;
using Clean.DataAccess.Entities.Base;

namespace Clean.DataAccess.Repositories.Base.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(params object[] id);
    }
}
