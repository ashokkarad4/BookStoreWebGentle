﻿using BookStoreWebGentle.Models;
using BookStoreWebGentle.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
namespace BookStoreWebGentle.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        private IHostingEnvironment _environment;

        public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment, IHostingEnvironment environment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
            _environment = environment;
        }

        [Route("all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }

        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        [Authorize]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);

            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        [Authorize(Roles ="SuperAdmin,Admin")]
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {

             if (ModelState.IsValid)
             {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                  bookModel.CoverImageUrl=  await UploadImage(folder,bookModel.CoverPhoto);

                }

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file),

                        };
                              bookModel.Gallery.Add(gallery);
                    }
                }


                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                } 
             }
                 ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

                   return View();
        }

        private async Task<string> UploadImage(string folderPath,IFormFile file)
        {
             folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;


            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;    
        }

        private List<LanguageModel> GetLanguage()
        {
            return new List<LanguageModel>()
            {
                new LanguageModel(){Id=1,Text="Hindi"},
                new LanguageModel(){Id=2,Text="English"},
                new LanguageModel(){Id=3,Text="Dutch"},
                new LanguageModel(){Id=4,Text="Urdu"},
                new LanguageModel(){Id=5,Text="Tamil"},
                new LanguageModel(){Id=6,Text="Kannad"},
                new LanguageModel(){Id=7,Text="Bangali"},

            };
        }
    }
}
