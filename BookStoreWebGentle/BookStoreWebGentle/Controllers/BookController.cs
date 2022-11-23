﻿using BookStoreWebGentle.Models;
using BookStoreWebGentle.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace BookStoreWebGentle.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

       public async Task<ViewResult> GetAllBooks()
         {   
            var data= await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-details/{id}",Name ="BookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {   
            var data= await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBook(string bookName,string AuthorName)
        {
            return _bookRepository.SearchBook(bookName, AuthorName);
        }
        public ViewResult AddNewBook(bool isSuccess=false, int bookId=0)
        {
            var model = new BookModel()
            {
              // Language = "English"
            };

            ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
            {
                Text=x.Text
            }).ToList();

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
                if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }

            }
            ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            return View();
         }

        private List<LanguageModel> GetLanguage()
        {
            return new List<LanguageModel>()
            {
                new LanguageModel(){Id=1,Text="Hindi"},
                new LanguageModel(){Id=2,Text="English"},
                new LanguageModel(){Id=3,Text="Dutch"},
            };
        }
    }
}
