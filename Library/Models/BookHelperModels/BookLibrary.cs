using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.BookHelperModels
{
    public class BookLibrary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Gener { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }

    }
}
