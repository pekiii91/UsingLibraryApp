using Library.Models;
using Library.Models.BookHelperModels;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.InterfaceService
{
    public interface IBookService
    {
        IResponse<List<BookHelperModel>> GetBooks();
        IResponse<List<BookLibrary>> GetBookLibrary();
        IResponse<NoValue> AddBook(BookModel book);
        BookModel GetBook(Guid id);
        IResponse<NoValue> EditBook(BookModel book);
        IResponse<NoValue> DeleteBook(Book book);
        IResponse<NoValue> BorrowsButton(BookModel model, string id);
        BookModel GetModal();
    }
}
