using System;
using System.Linq;
using Clean.DataAccess.Entities.Base;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clean.DataAccess.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CleanDbContext context;
        private readonly DbSet<T> dbSet;

        public BaseRepository(CleanDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbSet.Remove(entity);
        }

        public void Delete(params object[] id)
        {
            if (id == null || id.Length == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            T entity = dbSet.Find(id);
            Delete(entity);
        }
    }
}
