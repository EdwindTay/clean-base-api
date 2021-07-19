using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Clean.DataAccess.Core
{
    internal static class DbContextExtensions
    {
        internal static ModelBuilder ConfigureDatabase(this ModelBuilder builder, Assembly assembly)
        {
            //apply configurations using fluent api
            builder.ApplyConfigurationsFromAssembly(assembly);

            //restrict cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            return builder;
        }
    }
}
