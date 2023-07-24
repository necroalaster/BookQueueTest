using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Author(){
            Books = new HashSet<Book>();
        }

        public string Name
        {
            get;set;
        }

        public virtual ICollection<Book> Books { get; set; }
    }
}
