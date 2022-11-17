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
        [Route("book-details/{id}")]
        public ViewResult GetBook(int id)
        {
            var data= _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBook(string bookName,string AuthorName)
        {
            return _bookRepository.SearchBook(bookName, AuthorName);
        }
    }
}
