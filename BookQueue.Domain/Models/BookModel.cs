using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookQueue.Domain.Models
{
    public class BookModel
    {
        public Guid Id
        {
            get;set;
        }

        [Required]
        public string Title
        {
            get; set;
        }

        [Required]
        public int PublicationYear
        {
            get; set;
        }

        [Required]
        public string AuthorName
        {
            get; set;
        }
    }
}
