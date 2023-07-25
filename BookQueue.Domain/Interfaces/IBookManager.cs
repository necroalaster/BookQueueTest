using BookQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Interfaces
{
    public interface IBookManager
    {
        IEnumerable<BookModel> GetAllBooks();
        BookModel GetBookById(Guid id);
        void DeleteBook(Guid id);
        void SaveBook(BookModel book);
    }
}
