using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.CurrentHelperModels
{
    public class CurrentBorrowsStudent
    {
        public Guid Id { get; set; }
        public string Book { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
