using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Models
{
    public partial class CurrentBorrow
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public Guid ConfirmedBy { get; set; }
        public int StatusId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Status Status { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
