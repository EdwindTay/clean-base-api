using Clean.DataAccess.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.DataAccess.Entities.Base.Configuration
{
    internal static class IHasTenantConfigurationExtension
    {
        internal static EntityTypeBuilder<T> ConfigureTenantColumns<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity, IHasTenant
        {
            builder
                .HasOne(a => a.Tenant)
                .WithMany()
                .HasForeignKey(a => a.TenantId);

            return builder;
        }
    }
}
