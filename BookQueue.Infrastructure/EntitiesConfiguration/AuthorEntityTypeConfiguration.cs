using BookQueue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Infrastructure.EntitiesConfiguration
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(e => e.Id)
                    .HasColumnName("AuthorId")
                    .HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreatedAt).IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
        }
    }
}
