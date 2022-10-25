using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Infrastructure.Persistance.EntitiesConfiguration
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>

    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors")
                .HasKey(t => t.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CarrierDescription)
                .IsRequired(false);

            builder.Property(x => x.BirthDay)
                .IsRequired(false);

            builder.Property(x => x.Image)
                .IsRequired(false);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x=> x.AuthorId);
        }
    }
}
