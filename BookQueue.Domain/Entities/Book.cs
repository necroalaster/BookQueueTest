using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title
        {
            get;set;
        }

        public int PublicationYear
        {
            get;set;
        }

        public Guid AuthorId
        {
            get; set;
        }

        public virtual Author Author
        {
            get;
        }
    }
}
