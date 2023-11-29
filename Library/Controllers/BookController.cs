using Library.InterfaceService;
using Library.Models;
using Library.Models.BookHelperModels;
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
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAdminService _adminService;
        public BookController(IBookService bookService, IAdminService adminService)
        {
            _bookService = bookService;
            _adminService = adminService;
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult Index()
        {
            var result = _bookService.GetBooks();
            return View(result.Value);
        }

        [Authorize(Roles = "Librarian")]

        [HttpGet]
        public IActionResult LibraryListIndex()
        {
            var result = _bookService.GetBookLibrary();
            return View(result.Value);
        }

        [Authorize(Roles = "Librarian")]

        [HttpGet]
        public ActionResult Create()
        {
            var result = _bookService.GetModal();
            return PartialView("_CreateBookPartial", result);
        }

        [Authorize(Roles = "Librarian")]

        [HttpPost]
        public ActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                var result = _bookService.AddBook(book);
                return Json(result);
            }
            else
            {
                return View(book);
            }
        }

        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            BookModel book = _bookService.GetBook(id);  // implementirana u EmployeesRepository
            return PartialView("_EditPartial", book);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public ActionResult Edit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _bookService.EditBook(model);
                return Json(result);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public ActionResult Delete(Book book)
        {
            var result = _bookService.DeleteBook(book);
            return Json(result);
        }


        [Authorize(Roles = "Student")]
        //Borrows Student
        [HttpPost]
        public ActionResult AddBorrowsBook(BookModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _bookService.BorrowsButton(model, userId);
            return Json(result);
        }


    }
}
