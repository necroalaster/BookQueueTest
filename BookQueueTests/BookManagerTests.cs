using AutoMapper;
using BookQueue.Domain.Business;
using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Domain.Mappings;
using BookQueue.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BookQueueTests
{
    public class BookManagerTests
    {
        private readonly IMapper _mapper; 
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public BookManagerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BookProfile()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public void GetAllBooks_Test()
        {
            _bookRepositoryMock.Setup(x => x.GetAll()).Returns(GenerateBooks());

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            var bookList = _bookManager.GetAllBooks();

            Assert.NotNull(bookList);
            Assert.NotEmpty(bookList);
            Assert.Equal(3, bookList.Count());
            Assert.IsType<BookModel>(bookList.First());
        }

        [Fact]
        public void GetAllBooksReturnEmpty_Test()
        {
            _bookRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Book>());

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            var bookList = _bookManager.GetAllBooks();

            Assert.NotNull(bookList);
            Assert.Empty(bookList);
        }

        [Fact]
        public void GetBookById_Test()
        {
            _bookRepositoryMock.Setup(x => x.GetByID(It.IsAny<Guid>())).Returns(GenerateBooks().First());

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            var book = _bookManager.GetBookById(Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"));

            Assert.NotNull(book);
            Assert.IsType<BookModel>(book);
        }

        [Fact]
        public void GetBookByIdReturnNull_Test()
        {
            _bookRepositoryMock.Setup(x => x.GetByID(It.IsAny<Guid>())).Returns(ReturnBookBull());

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            var book = _bookManager.GetBookById(Guid.Parse("EB9C162B-C25F-4450-B9C8-8A3A55378ADA"));

            Assert.Null(book);
        }

        [Fact]
        public void SaveBookNewAuthor_Test()
        {
            var book = new BookModel()
            {
                AuthorName = "Arthur Conan Doyle",
                PublicationYear = 1960,
                Title = "Sherlocke Holmes",
                Id = Guid.Empty
            };

            var author = new Author() {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = book.AuthorName
            };

            _bookRepositoryMock.Setup(x => x.Save(It.IsAny<Book>()));
            _authorRepositoryMock.Setup(x => x.Save(It.IsAny<Author>())).Verifiable();
            _authorRepositoryMock.SetupSequence(x => x.GetByname(It.IsAny<string>())).Returns(ReturnAuthorBull()).Returns(author);

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            _bookManager.SaveBook(book);

            _authorRepositoryMock.Verify(x => x.Save(It.IsAny<Author>()), Times.Once);
            _authorRepositoryMock.Verify(x => x.GetByname(It.IsAny<string>()), Times.Exactly(2));
            _bookRepositoryMock.Verify(x => x.Save(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void SaveBookExistingAuthor_Test()
        {
            var book = new BookModel()
            {
                AuthorName = "Arthur Conan Doyle",
                PublicationYear = 1960,
                Title = "Sherlocke Holmes",
                Id = Guid.Empty
            };

            var author = new Author()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = book.AuthorName
            };

            _bookRepositoryMock.Setup(x => x.Save(It.IsAny<Book>()));
            _authorRepositoryMock.Setup(x => x.Save(It.IsAny<Author>())).Verifiable();
            _authorRepositoryMock.SetupSequence(x => x.GetByname(It.IsAny<string>())).Returns(author);

            var _bookManager = new BookManager(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper);

            _bookManager.SaveBook(book);

            _authorRepositoryMock.Verify(x => x.Save(It.IsAny<Author>()), Times.Never);
            _authorRepositoryMock.Verify(x => x.GetByname(It.IsAny<string>()), Times.Once);
            _bookRepositoryMock.Verify(x => x.Save(It.IsAny<Book>()), Times.Once);
        }

        private IEnumerable<Book> GenerateBooks()
        {
            Author author = new Author()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = "John Doe"
            };

            return new List<Book>()
            {
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
            };
        }

        private Book ReturnBookBull()
        {
            return null;
        }

        private Author ReturnAuthorBull()
        {
            return null;
        }
    }
}
