using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.BookHelperModels
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Gener { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }
        public int StatusId { get; set; }
   
        public List<SelectListItem> StatusList { get; set; }


    }
}
