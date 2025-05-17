using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SK.Visit.Domain.Entities;
using SK.Visit.Infrastructure.SqlServer.Configuration;
using SK.Visit.Application.Interfaces.Common;

namespace SK.Visit.Infrastructure.SqlServer.Persistence
{
    public class VisitDbContext : DbContext
    {
        public VisitDbContext(DbContextOptions<VisitDbContext> options) : base(options) { }
        private readonly ICurrentUserService? _currentUserService;

        public VisitDbContext(DbContextOptions<VisitDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Destination> Destinations { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VisitConfiguration());
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService?.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
