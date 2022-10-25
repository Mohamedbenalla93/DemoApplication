using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Domain;
using DemoApplication.Core.Domain.Entities;
using DemoApplication.Infrastructure.Persistance.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Infrastructure.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Gernes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookGenres> BookGenres { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<BaseEntityWithId>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete)
                        {
                            entry.State = EntityState.Modified;
                            entry.Entity.IsDeleted = true;
                            entry.Entity.DeletedAt = DateTime.Now;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //String values to empty string and not a null
                var properties = entityType.GetProperties().Where(p => p.ClrType == typeof(string));
                foreach (var property in properties)
                {
                    property.SetDefaultValue("");
                }
            }


            foreach (var entityType in builder.Model.GetEntityTypes()
                         .Where(entity => typeof(BaseEntityWithId).IsAssignableFrom(entity.ClrType)))
            {
                foreach (var property in entityType.ClrType.GetProperties()
                             .Where(p => p.PropertyType == typeof(Guid) && p.Name == "Id"))
                {
                    builder
                        .Entity(entityType.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("newsequentialid()");
                }
            }

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //other automated configurations left out
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            //Apply all configurations in assembly
            builder.ApplyConfigurationsFromAssembly(typeof(AuthorEntityConfiguration).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
