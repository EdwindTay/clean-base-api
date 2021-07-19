using Clean.Core.Entities.Users;
using Clean.DataAccess.Entities.Base;
using Clean.DataAccess.Entities.Base.Configuration;
using Clean.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.DataAccess.Entities.Users
{
    public class Tenant : BaseEntity
    {
        public long TenantId { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// EF - Fluent Api Configuration
    /// </summary>
    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable(name: "Tenant", schema: Schemas.dbo);

            builder.HasKey(a => a.TenantId);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(TenantPropertiesConfiguration.NameMaxLength);

            builder.ConfigureAuditColumns();
        }
    }
}
