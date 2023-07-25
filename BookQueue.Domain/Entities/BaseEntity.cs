using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id
        {
            get;set;
        }

        public DateTime CreatedAt
        { 
            get; set; 
        }

        public DateTime ModifiedAt
        {
            get;set;
        }
    }
}
