using BookQueue.Domain.Entities;
using BookQueue.Domain.Interfaces;
using BookQueue.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookQueue.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private BookQueueContext _context;

        public AuthorRepository(BookQueueContext context)
        {
            _context = context;
        }

        public Author GetByname(string name)
        {
            return _context.Authors.Where(x => x.Name == name).SingleOrDefault();
        }

        public void Save(Author author)
        {
            var oldAuthor = _context.Authors.Where(x => x.Id == author.Id).SingleOrDefault();

            if (oldAuthor != null)
            {
                oldAuthor.Name = author.Name;
            }
            else
                _context.Authors.Add(author);

            _context.SaveChanges();
        }
    }
}
