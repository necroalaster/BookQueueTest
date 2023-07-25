using BookQueue.Domain.Entities;
using BookQueue.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Infrastructure.Context
{
    public class BookQueueContext : DbContext
    {

        public BookQueueContext()
        {

        }

        public BookQueueContext(DbContextOptions<BookQueueContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());

            new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
        }
    }
}
