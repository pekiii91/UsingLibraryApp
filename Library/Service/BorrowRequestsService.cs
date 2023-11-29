using Library.InterfaceService;
using Library.Models;
using Library.Models.BorrowRequestsHelperModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class BorrowRequestsService : IBorrowRequestsService
    {
        private readonly LibraryDBContext _context;

        public BorrowRequestsService(LibraryDBContext context)
        {
            _context = context;
        }

        public IResponse<List<BorrowRequestsHelperModel>> GetRequests()
        {
            var response = new Response<List<BorrowRequestsHelperModel>>();

            BorrowRequest borrowRequest = new BorrowRequest();
            try
            {
                IQueryable<BorrowRequest> requests = _context.BorrowRequests.Where(br => br.StatusId == (int)StatusRequest.Pending); //samo sa StatusId = 14 = "Pending"
                //var test = _context.BorrowRequests.Where(br => br.StatusId == 14);
                response.Value = requests.Select(br => new BorrowRequestsHelperModel // ovde sam pozvao test promenjivu za Listu i ovde idu svi sa statusom Pending
                {
                    Id = br.Id,
                    Book = br.Book.Name,
                    User = br.User.FirstName + " " + br.User.LastName,
                    StartDate = br.StartDate,
                    EndDate = br.EndDate,
                    Status = br.Status.StatusName
                }).ToList();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
            
        }

        public BorrowRequest GetBorrowRequestById(Guid id)
        {
            return _context.BorrowRequests.Where(r => r.Id == id).FirstOrDefault();
        }

        public IResponse<NoValue> AcceptBorrow(BorrowRequest model, string id)
        {
            var response = new Response<NoValue>();
            try
            {
                BorrowRequest borrowRequest = _context.BorrowRequests.Where(br => br.Id == model.Id).FirstOrDefault();
                borrowRequest.StatusId = model.StatusId; //ovde sam promenio Status Name
                borrowRequest.Status = _context.Statuses.Where(s => s.StatusId == borrowRequest.StatusId).FirstOrDefault();
            

                var book = _context.CurrentBorrows.Where(br => br.BookId == borrowRequest.BookId).FirstOrDefault();
                if (book != null)
                {
                    _context.CurrentBorrows.Remove(book);

                }

                CurrentBorrow currentBorrow = new CurrentBorrow()
                {
                    Id = Guid.NewGuid(),
                    BookId = borrowRequest.BookId,
                    UserId = new Guid(id),
                    StartDate = borrowRequest.StartDate,
                    EndDate = borrowRequest.EndDate,
                    ConfirmedBy = borrowRequest.UserId,
                    StatusId = borrowRequest.StatusId
                };
                _context.CurrentBorrows.Add(currentBorrow);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went wrong!";
            }
            return response;
        }

        public IResponse<NoValue> DeclineStatus(BorrowRequest model, string id)
        {
            var response = new Response<NoValue>();
            try
            {
                BorrowRequest borrowRequest = _context.BorrowRequests.Where(r => r.Id == model.Id).FirstOrDefault();
                borrowRequest.StatusId = model.StatusId; //ovde sam promenio StatusId 
                borrowRequest.Status = _context.Statuses.Where(s => s.StatusId == borrowRequest.StatusId).FirstOrDefault();

                var book = _context.CurrentBorrows.Where(br => br.BookId == borrowRequest.BookId).FirstOrDefault();
                if (book != null)
                {
                    _context.CurrentBorrows.Remove(book);
                }


                CurrentBorrow currentBorrow = new CurrentBorrow()
                {
                    Id = Guid.NewGuid(),
                    BookId = borrowRequest.BookId,
                    UserId = new Guid(id),
                    StartDate = borrowRequest.StartDate,
                    EndDate = borrowRequest.EndDate,
                    ConfirmedBy = borrowRequest.UserId,
                    StatusId = borrowRequest.StatusId
                };
                _context.CurrentBorrows.Add(currentBorrow);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went wrong!";
            }
            return response;
        }

     
    }
}
