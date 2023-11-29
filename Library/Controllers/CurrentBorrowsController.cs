using Library.InterfaceService;
using Library.Models;
using Library.Models.CurrentHelperModels;
using Library.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = "Librarian, Student")]

    public class CurrentBorrowsController : Controller
    {
        private readonly ICurentBorrowsService _currentBorrowsService;

        public CurrentBorrowsController(ICurentBorrowsService curentBorrowsService)
        {
            _currentBorrowsService = curentBorrowsService;
        }

        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _currentBorrowsService.GetCurrentBorrows(userId);
            return View(result.Value);
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult CurrentStatusIndex()
        {
            var result = _currentBorrowsService.GetCurrentBorrowsStatus();
            return View(result.Value);
        }



    }
}
