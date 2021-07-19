using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.DataAccess.Entities.Base.Configuration
{
    internal static class BaseEntityConfigurationExtension
    {
        internal static EntityTypeBuilder<T> ConfigureAuditColumns<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder
                .HasOne(a => a.CreatedBy)
                .WithMany()
                .HasForeignKey(a => a.CreatedById);

            builder
                .HasOne(a => a.UpdatedBy)
                .WithMany()
                .HasForeignKey(a => a.UpdatedById);

            return builder;
        }
    }
}
