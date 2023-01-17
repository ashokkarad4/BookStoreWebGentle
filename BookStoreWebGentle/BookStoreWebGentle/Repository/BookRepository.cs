using BookStoreWebGentle.Data;
using BookStoreWebGentle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Category=model.Category,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Price = (double)model.Price,
                Language = model.Language,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     Language = book.Language,
                     Title = book.Title,
                     Price = (int?)book.Price,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl
                 }).ToListAsync();
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      Id = book.Id,
                      Language = book.Language,
                      Title = book.Title,
                      Price = (int?)book.Price,
                      TotalPages = book.TotalPages,
                      CoverImageUrl = book.CoverImageUrl
                  }).Take(count).ToListAsync();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {  
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     Language = book.Language,
                     Title = book.Title,
                     Price = (int?)book.Price,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl,
                     Gallery = book.bookGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList(),
                     BookPdfUrl = book.BookPdfUrl
                 }).FirstOrDefaultAsync();
        }
        public async Task<int> DeleteBook(int? id)
        {
            var result = await _context.Books.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.Books.Remove(result);
                await _context.SaveChangesAsync();      
            }
            return result.Id;
        }
        //public async Task<IEnumerable<BookModel>> Search(string title, string author)
        //{
        //    IQueryable<BookModel> query = (IQueryable<BookModel>)_context.Books;
        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        query = query.Where(e => e.Title.Contains(title));
        //    }
        //    if (author != null)
        //    {
        //        query = query.Where(e => e.Author == author);
        //    }
        //    return await query.ToListAsync();
        //}

        public string GetAppName()
        {
            return "Book Store Application";
        }

    }
}
