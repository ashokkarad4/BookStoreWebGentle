using BookStoreWebGentle.Models;
using BookStoreWebGentle.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController()
        {
            _bookRepository = new BookRepository();
        }
       public ViewResult GetAllBooks()
        {
            var data= _bookRepository.GetAllBooks();
            return View(data);
        }
        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }
        public List<BookModel> SearchBook(string bookName,string AuthorName)
        {
            return _bookRepository.SearchBook(bookName, AuthorName);
        }
    }
}
