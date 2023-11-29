using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Models
{
    public partial class Status
    {
        public Status()
        {
            Books = new HashSet<Book>();
            BorrowRequests = new HashSet<BorrowRequest>();
            CurrentBorrows = new HashSet<CurrentBorrow>();
        }

        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<BorrowRequest> BorrowRequests { get; set; }
        public virtual ICollection<CurrentBorrow> CurrentBorrows { get; set; }
    }
}
