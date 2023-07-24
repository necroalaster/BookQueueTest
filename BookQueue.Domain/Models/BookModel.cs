using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Models
{
    public class BookModel
    {
        public Guid Id
        {
            get;set;
        }

        public string Title
        {
            get; set;
        }

        public int PublicationYear
        {
            get; set;
        }

        public string AuthorName
        {
            get; set;
        }
    }
}
