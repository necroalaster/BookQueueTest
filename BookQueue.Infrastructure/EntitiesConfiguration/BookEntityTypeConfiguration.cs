using BookQueue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Infrastructure.EntitiesConfiguration
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.Id)
                    .HasColumnName("BookID")
                    .HasDefaultValueSql("(newid())");

            builder.Property(e => e.PublicationYear).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.AuthorId).IsRequired();

            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            builder.HasOne(d => d.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Author_Book");
        }
    }
}
