using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookQueue.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private BookQueueContext _context;

        public BookRepository(BookQueueContext context)
        {
            _context = context;
        }

        public void Delete(Guid bookId)
        {
            var book = _context.Books.Where(x => x.Id == bookId).SingleOrDefault();

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
                
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .OrderBy(x => x.Title)
                .AsNoTracking();
        }

        public Book GetByID(Guid bookId)
        {
            return _context.Books.Where(x => x.Id == bookId).SingleOrDefault();
        }

        public void Save(Book book)
        {
            var oldBook = _context.Books.Where(x => x.Id == book.Id).SingleOrDefault();

            if (oldBook != null)
            {
                oldBook.PublicationYear = book.PublicationYear;
                oldBook.Title = book.Title;
            }
            else
                _context.Books.Add(book);

            _context.SaveChanges();
        }
    }
}
