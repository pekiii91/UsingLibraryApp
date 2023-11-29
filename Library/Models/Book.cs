using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Models
{
    public partial class Book
    {
        public Book()
        {
            BorrowRequests = new HashSet<BorrowRequest>();
            CurrentBorrows = new HashSet<CurrentBorrow>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Gener { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<BorrowRequest> BorrowRequests { get; set; }
        public virtual ICollection<CurrentBorrow> CurrentBorrows { get; set; }
    }
}
