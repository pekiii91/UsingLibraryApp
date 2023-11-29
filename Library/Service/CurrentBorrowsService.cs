using Library.InterfaceService;
using Library.Models;
using Library.Models.CurrentHelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class CurrentBorrowsService : ICurentBorrowsService
    {
        private readonly LibraryDBContext _context;

        public CurrentBorrowsService(LibraryDBContext context)
        {
            _context = context;
        }

        //List Librarian
        public IResponse<List<CurrentBorrowsLibrarian>> GetCurrentBorrows(string id)
        {
            var response = new Response<List<CurrentBorrowsLibrarian>>();
            try
            {
                IQueryable<CurrentBorrow> test = _context.CurrentBorrows.OrderBy(c => c.StartDate); //sortiram po datumu
                response.Value = test.Select(c => new CurrentBorrowsLibrarian
                {
                    Id = c.Id,
                    Book = c.Book.Name,
                    User = c.User.FirstName + " " + c.User.LastName,
                    StartDate = c.StartDate, //za start date
                    EndDate = c.EndDate, //za end date
                    ConfirmedBy = _context.AspNetUsers.Where(u=>u.Id == c.ConfirmedBy).FirstOrDefault().FirstName + " " + _context.AspNetUsers.Where(u => u.Id == c.ConfirmedBy).FirstOrDefault().LastName
                }).ToList();
                response.Status = StatusEnum.Success;

            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

        //List Student
        public IResponse<List<CurrentBorrowsStudent>> GetCurrentBorrowsStatus()
        {
            var response = new Response<List<CurrentBorrowsStudent>>();
            try
            {
                IQueryable<CurrentBorrow> test = _context.CurrentBorrows.OrderBy(c => c.StartDate); //sortiram po datumu

                response.Value = test.Select(c => new CurrentBorrowsStudent
                {
                    Id = c.Id,
                    Book = c.Book.Name,
                    StartDate = c.StartDate, //za start date
                    EndDate = c.EndDate, //za end date
                    Status = c.Status.StatusName
                }).ToList();

                response.Status = StatusEnum.Success;

            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

    }
}
