using BookQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        void Save(Author author);

        Author GetByname(string name);
    }
}
