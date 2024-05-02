using Microsoft.EntityFrameworkCore;
using RealEstatePlatform.Application.Contracts.Interfaces;
using RealEstatePlatform.Domain.Common;
using RealEstatePlatform.Domain.Entities;
using System.Security.Claims;

namespace RealEstatePlatform.Infrastructure
{
    public class RealEstatePlatformContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;
        public RealEstatePlatformContext(DbContextOptions<RealEstatePlatformContext> options, ICurrentUserService currentUserService) : base(options)
        {
            this.currentUserService = currentUserService;
        }
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<PropertyType> PropertyTypes { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<ListingType> ListingTypes { get; set; } = null!;

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
