using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clean.DataAccess.Core
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return query.Where(predicate);
            }
            else
            {
                return query;
            }
        }
    }
}
