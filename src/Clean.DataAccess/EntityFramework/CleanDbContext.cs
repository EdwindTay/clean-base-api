using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clean.DataAccess.Core;
using Clean.DataAccess.Core.Context;
using Clean.DataAccess.Entities.Base;
using Clean.DataAccess.Entities.Base.Interfaces;
using Clean.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using static Clean.Core.Enums;

namespace Clean.DataAccess.EntityFramework
{
    public class CleanDbContext : DbContext
    {
        private readonly IUserContext _userContext;

        public CleanDbContext(DbContextOptions<CleanDbContext> options, IUserContext userContext) : base(options)
        {
            _userContext = userContext;
        }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureDatabase(typeof(CleanDbContext).Assembly);

            modelBuilder.SetQueryFilterOnAllEntities<BaseEntity>(x => x.ActiveStatus != (int)ActiveStatus.Deleted);
            modelBuilder.SetQueryFilterOnAllEntities<IHasTenant>(x => _userContext.GetTenantId() == null || x.TenantId == _userContext.GetTenantId());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            UpdateAuditColumns();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditColumns();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditColumns()
        {
            var modifiedEntries = ChangeTracker.Entries()
                                    .Where(a => a.Entity is BaseEntity && (a.State == EntityState.Added || a.State == EntityState.Modified || a.State == EntityState.Deleted));

            long? userId = _userContext.GetUserId();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    var now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        //tenancy logic
                        if (entry.Entity is IHasTenant iHasTenantEntity)
                        {
                            //if user is tied to a tenant and tenantid is not passed from business logic layer, use the tenantid tied to user
                            if (iHasTenantEntity.TenantId == default && _userContext.GetTenantId().HasValue)
                            {
                                iHasTenantEntity.TenantId = _userContext.GetTenantId().Value;
                            }
                        }

                        if (entity.ActiveStatus == default)
                        {
                            entity.ActiveStatus = (int)ActiveStatus.Active;
                        }

                        if (entity.CreatedById == default && userId.HasValue)
                        {
                            entity.CreatedById = userId.Value;
                        }

                        entity.CreatedUtcTime = now;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entity.ActiveStatus = (int)ActiveStatus.Deleted;
                    }

                    if (entity.UpdatedById == default && userId.HasValue)
                    {
                        entity.UpdatedById = userId.Value;
                    }

                    entity.UpdatedUtcTime = now;
                }
            }
        }
    }
}
