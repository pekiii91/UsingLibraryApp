using Library.InterfaceService;
using Library.Models;
using Library.Models.BorrowRequestsHelperModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = "Librarian")]

    public class BorrowRequestsController : Controller
    {
        private readonly IBorrowRequestsService _borrowRequestsService;
        private readonly LibraryDBContext _context;

        public BorrowRequestsController(IBorrowRequestsService borrowRequestsService, LibraryDBContext context)
        {
            _context = context;
            _borrowRequestsService = borrowRequestsService;
        }

        public IActionResult Index()
        {
            var result = _borrowRequestsService.GetRequests();
            return View(result.Value);
        }

        //Button Accept
        [HttpPost]
        public ActionResult AcceptRequest(BorrowRequest model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _borrowRequestsService.AcceptBorrow(model, userId);

            return Json(result);
        }

        //Button Decline
        [HttpPost]
        public ActionResult DeclineRequest(BorrowRequest model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _borrowRequestsService.DeclineStatus(model, userId);
            return Json(result);
        }

     

    }
}
