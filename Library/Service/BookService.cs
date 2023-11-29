using Library.InterfaceService;
using Library.Models;
using Library.Models.BookHelperModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly LibraryDBContext _context;

        public BookService(LibraryDBContext context)
        {
            _context = context;
        }


        public IResponse<List<BookHelperModel>> GetBooks()
        {
            var response = new Response<List<BookHelperModel>>();
            try
            {
                //proveravam za svaku knjigu da li je ima u Current Borrows
                var currentBorrows = _context.CurrentBorrows.Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now).ToList();
                                                                        //|| c.StartDate > DateTime.Now && c.EndDate < DateTime.Now).ToList();                                                                //|| c.StartDate > DateTime.Now && c.EndDate < DateTime.Now).ToList();

                List<Book> books = new List<Book>();
                foreach (var curBorr in currentBorrows)
                {
                    books.Add(_context.Books.Where(b => b.Id == curBorr.BookId).FirstOrDefault());
                }

                //da mi izbaci ostale knjige koje nisu rezervisane
                List<Book> newBooks = new List<Book>();
                newBooks = _context.Books.ToList();
                foreach (var b in books)
                {
                    newBooks.Remove(b);
                }

                response.Value = books.Select(b => new BookHelperModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Writer = b.Writer,
                    Gener = b.Gener,
                    Published = b.Published,
                    Publisher = b.Publisher,
                    Status = ((StatusRequest)b.StatusId).ToString()
                }).ToList();

                response.Value = newBooks.Select(b => new BookHelperModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Writer = b.Writer,
                    Gener = b.Gener,
                    Published = b.Published,
                    Publisher = b.Publisher,
                    //StartDate = DateTime.Now,
                    //EndDate = DateTime.Now.AddDays(14),
                    Status = ((StatusRequest)b.StatusId).ToString()
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

        public IResponse<List<BookLibrary>> GetBookLibrary()
        {
            var response = new Response<List<BookLibrary>>();
            try
            {
                response.Value = _context.Books.Select(b => new BookLibrary
                {
                    Id = b.Id,
                    Name = b.Name,
                    Writer = b.Writer,
                    Gener = b.Gener,
                    Published = b.Published,
                    Publisher = b.Publisher
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

        public BookModel GetModal() // za dodavanje liste zaposlenih 
        {
            List<SelectListItem> statuses =  _context.Statuses.Select(s => new SelectListItem
            {
                Text = s.StatusName,
                Value = s.StatusId.ToString()
            }).ToList();

            var result = new BookModel
            {
                StatusList = statuses
            };
            return result;
        }

        public BookModel GetBook(Guid id)
        {   
           Book book = _context.Books.Where(b => b.Id == id).FirstOrDefault();
           BookModel bookModel = new BookModel
           {
               Id = book.Id,
               Name = book.Name,
               Writer = book.Writer,
               Gener = book.Gener,
               Published = book.Published,
               Publisher = book.Publisher,
               StatusId = book.StatusId,
               StatusList = _context.Statuses.Select(s => new SelectListItem
               {
                   Text = s.StatusName,
                   Value = s.StatusId.ToString()
               }).ToList()
            };
           return bookModel;
        }

        public IResponse<NoValue> AddBook(BookModel model)
        {
            var response = new Response<NoValue>();
            try
            {
                Book book = new Book
                {
                    Id = Guid.NewGuid(), //dodali Id Book
                    Name = model.Name,
                    Writer = model.Writer,
                    Gener = model.Gener,
                    Published = model.Published,
                    Publisher = model.Publisher,
                    StatusId = model.StatusId
                 
                };
                _context.Books.Add(book);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went wrong";
            }
            return response;
        }

        public IResponse<NoValue> EditBook(BookModel model)
        {
            var response = new Response<NoValue>();
            try
            {
                Book book = new Book
                {
                    Id = Guid.NewGuid(), //dodali Id Book
                    Name = model.Name,
                    Writer = model.Writer,
                    Gener = model.Gener,
                    Published = model.Published,
                    Publisher = model.Publisher,
                    StatusId = model.StatusId
                };
                _context.Books.Update(book);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went wrong";
            }
            return response;
            
        }

        public IResponse<NoValue> DeleteBook(Book book)
        {
            var response = new Response<NoValue>();
            try
            {
                _context.Remove(book);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Cancel";
            }
            return response;
        }

        // Button Borrows from Book(Student) in the Borrow Requests
        public IResponse<NoValue> BorrowsButton(BookModel model, string id)
        {
            var response = new Response<NoValue>();
            try
            {
                Book newBooks = _context.Books.Where(b => b.Id == model.Id).FirstOrDefault();

                BorrowRequest borrowRequest = new BorrowRequest()
                {
                    Id = Guid.NewGuid(), 
                    BookId = newBooks.Id,
                    UserId = new Guid(id),
                    StatusId = newBooks.StatusId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14) 
                };
                _context.BorrowRequests.Add(borrowRequest);
                _context.SaveChanges();
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong.";
            }
            return response;
        }


    }
}
