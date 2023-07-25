using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Infrastructure.Context;
using BookQueue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookQueueTests
{
    [Collection("Repository Tests")]
    public class BookRepositoryTests
    {
        private BookQueueContext _context;
        private IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<BookQueueContext>()
                .UseInMemoryDatabase("BookQueueTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new BookQueueContext(contextOptions);
            _bookRepository = new BookRepository(_context);
        }

        [Fact]
        public void GetAll_Test()
        {
            GenerateBooks();

            var books = _bookRepository.GetAll();

            Assert.NotNull(books);
            Assert.NotEmpty(books);
            Assert.Equal(3, books.Count());
        }

        [Fact]
        public void GetAllReturnsNull_Test() 
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var books = _bookRepository.GetAll();

            Assert.NotNull(books);
            Assert.Empty(books);
        }

        [Fact]
        public void GetById_Test()
        {
            GenerateBooks();
            Guid searchedId = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA");

            var book = _bookRepository.GetByID(searchedId);

            Assert.NotNull(book);
            Assert.Equal(searchedId, book.Id);
        }

        [Fact]
        public void GetByIdReturnsNull_Test()
        {
            GenerateBooks();
            Guid searchedId = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378AD1");

            var book = _bookRepository.GetByID(searchedId);

            Assert.Null(book);
        }

        [Fact]
        public void Add_Test()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var authorId = Guid.NewGuid();

            _bookRepository.Save(new Book()
            {
                Id = Guid.NewGuid(),
                Title = "O Mago",
                PublicationYear = 1964,
                Author = new Author()
                {
                    Id = authorId,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Name = "John Doe"
                },
                AuthorId = authorId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            });

            Assert.NotNull(_context.Books);
            Assert.NotEmpty(_context.Books);
            Assert.Equal(1, _context.Books.Count());
        }

        [Fact]
        public void Update_Test()
        {
            GenerateBooks();

            Author author = new Author()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = "Joanna Doe"
            };

            var book = new Book()
            {
                Id = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"),
                Title = "A Neo Arte Moderna",
                PublicationYear = 1990,
                Author = author,
                AuthorId = author.Id,
                ModifiedAt = DateTime.Now
            };

            _bookRepository.Save(book);

            Assert.NotNull(_context.Books);
            Assert.NotEmpty(_context.Books);
            Assert.Equal(3, _context.Books.Count());

            var updatedBook = _bookRepository.GetByID(book.Id);

            Assert.Equal("A Neo Arte Moderna", updatedBook.Title);
            Assert.Equal(1990, updatedBook.PublicationYear);
        }

        [Fact]
        public void Delete_Test()
        {
            GenerateBooks();

            _bookRepository.Delete(Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"));

            Assert.NotNull(_context.Books);
            Assert.NotEmpty(_context.Books);
            Assert.Equal(2, _context.Books.Count());

            var updatedBook = _bookRepository.GetByID(Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"));

            Assert.Null(updatedBook);
        }

        private void GenerateBooks()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            Author author = new Author()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = "John Doe"
            };

            _context.Books.AddRange(
                new Book()
                {
                    Id = Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"),
                    Title = "A Arte Moderna",
                    PublicationYear = 1981,
                    Author = author,
                    AuthorId = author.Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "O Mago",
                    PublicationYear = 1964,
                    Author = author,
                    AuthorId = author.Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "A Cachoeira",
                    PublicationYear = 1973,
                    Author = author,
                    AuthorId = author.Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
            );

            _context.SaveChanges();
        }
    }
}
