using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Infrastructure.Context;
using BookQueue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BookQueueTests
{
    [Collection("Repository Tests")]
    public class AuthorRepositoryTests
    {
        private BookQueueContext _context;
        private IAuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<BookQueueContext>()
                .UseInMemoryDatabase("BookQueueTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new BookQueueContext(contextOptions);
            _authorRepository = new AuthorRepository(_context);
        }

        [Fact]
        public void GetByName_Test()
        {
            GenerateAuthors();

            var author = _authorRepository.GetByname("erico verissímo");

            Assert.NotNull(author);
            Assert.Equal("Erico Verissímo", author.Name);
        }

        [Fact]
        public void GetByNameReturnsNull_Test()
        {
            GenerateAuthors();

            var author = _authorRepository.GetByname("Joahnna");

            Assert.Null(author);
        }

        [Fact]
        public void Add_Test()
        {
            GenerateAuthors();

            var author = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Joahnna Doe",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _authorRepository.Save(author);

            Assert.NotNull(_context.Authors);
            Assert.NotEmpty(_context.Authors);
            Assert.Equal(3, _context.Authors.Count());
        }

        [Fact]
        public void Update_Test()
        {
            GenerateAuthors();

            var author = new Author()
            {
                Id = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"),
                Name = "Joahnna Doe",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _authorRepository.Save(author);

            Assert.NotNull(_context.Authors);
            Assert.NotEmpty(_context.Authors);
            Assert.Equal(2, _context.Authors.Count());

            var updatedAuthor = _authorRepository.GetByname(author.Name);

            Assert.Equal("Joahnna Doe", updatedAuthor.Name, true);
        }

        private void GenerateAuthors()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Authors.AddRange(
                new Author()
                {
                    Id = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"),
                    Name = "John Doe",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Author()
                {
                    Id = Guid.NewGuid(),
                    Name = "Erico Verissímo",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
            );

            _context.SaveChanges();
        }
    }
}
