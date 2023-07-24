using BookQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Interfaces
{
    public interface IBookRepository
    {
        Book GetByID(Guid id);

        IEnumerable<Book> GetAll();

        void Save(Book book);

        void Delete(Guid Id);
    }
}
