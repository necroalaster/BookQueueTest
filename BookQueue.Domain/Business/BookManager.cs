using AutoMapper;
using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookQueue.Domain.Business
{
    public class BookManager : IBookManager
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        
        public BookManager(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            var bookList = _bookRepository.GetAll();

            return _mapper.Map<List<Book>, List<BookModel>>(bookList.ToList());
        }

        public BookModel GetBookById(Guid id)
        {
            var book = _bookRepository.GetByID(id);

            return _mapper.Map<BookModel>(book);
        }

        public void DeleteBook(Guid id)
        {
            _bookRepository.Delete(id);
        }

        public void SaveBook(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            var author = _authorRepository.GetByname(bookModel.AuthorName);

            if (author == null)
            {
                author = CreateAuthor(bookModel.AuthorName);
            }

            book.Author = author;
            book.AuthorId = author.Id;

            _bookRepository.Save(book);
        }

        private Author CreateAuthor(string authorName)
        {
            _authorRepository.Save(
                new Author()
                {
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Name = authorName
                }
            );

            return _authorRepository.GetByname(authorName);
        }
    }
}
