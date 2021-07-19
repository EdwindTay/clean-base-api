using Clean.Core.Entities.Users;
using Clean.DataAccess.Entities.Base;
using Clean.DataAccess.Entities.Base.Configuration;
using Clean.DataAccess.Entities.Base.Interfaces;
using Clean.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.DataAccess.Entities.Users
{
    public class User : BaseEntity, IHasTenant
    {
        public User()
        {
        }

        public long UserId { get; set; }
        public string ExternalUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public long? TenantId { get; set; }

        //navigation properties
        public Tenant Tenant { get; set; }
    }

    /// <summary>
    /// EF - Fluent Api Configuration
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "User", schema: Schemas.dbo);

            builder.HasKey(a => a.UserId);

            builder.Property(a => a.ExternalUserId)
                .IsRequired()
                .HasMaxLength(UserPropertiesConfiguration.ExternalUserIdMaxLength);

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(UserPropertiesConfiguration.UserNameMaxLength);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(UserPropertiesConfiguration.EmailMaxLength);

            builder.Property(a => a.DisplayName)
                .IsRequired()
                .HasMaxLength(UserPropertiesConfiguration.DisplayNameMaxLength);

            builder.HasIndex(a => a.ExternalUserId);
            builder.HasIndex(a => a.UserName);
            builder.HasIndex(a => a.Email);

            builder.ConfigureAuditColumns();
            builder.ConfigureTenantColumns();
        }
    }
}
