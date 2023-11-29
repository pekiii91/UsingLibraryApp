using Library.Models;
using Library.Models.BorrowRequestsHelperModels;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.InterfaceService
{
    public interface IBorrowRequestsService
    {
        IResponse<List<BorrowRequestsHelperModel>> GetRequests();
        BorrowRequest GetBorrowRequestById(Guid id);
        IResponse<NoValue> AcceptBorrow(BorrowRequest model, string id);
        IResponse<NoValue> DeclineStatus(BorrowRequest model, string id);
        //IResponse<NoValue> AddComponentToBorrow(BorrowRequest model);

    }
}
