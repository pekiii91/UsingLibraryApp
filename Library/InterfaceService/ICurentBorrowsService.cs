using Library.Models;
using Library.Models.CurrentHelperModels;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.InterfaceService
{
    public interface ICurentBorrowsService
    {
        IResponse<List<CurrentBorrowsLibrarian>> GetCurrentBorrows(string id);
        IResponse<List<CurrentBorrowsStudent>> GetCurrentBorrowsStatus();
       // IResponse<NoValue> BookButton(CurrentBorrow model);
    }
}
