using BookStoreWebGentle.Data;
using BookStoreWebGentle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public class BookRepository
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
                //BookRepository
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value:0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Price = (double)model.Price,
                Language = model.Language,
                Category=model.Category


            };
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();
            return newBook.Id;
        }
        
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any()==true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Title = book.Title,
                        Price = (int?)book.Price,
                        TotalPages = book.TotalPages,
                         
                    });
                }
            }
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var book = await  _context.Books.FindAsync(id);
            if (book !=null)
            {
                var bookDetails = new BookModel()
                {  
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    Price = (int?)book.Price,
                    TotalPages = book.TotalPages,
                };
                return bookDetails;
            }
            // _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            return null;    
        }

        public List<BookModel> SearchBook(string title,string authorName)
        {
            return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="MVC",Author="Ashok",Description="This is the descrption of MVC Book",Category="Programming",Language="English",TotalPages=463},
                new BookModel(){Id=2,Title="C#",Author="Deepak",Description="This is the descrption of C# Book",Category="Developer backend",Language="English",TotalPages=459},
                new BookModel(){Id=3,Title="Java",Author="Onkar",Description="This is the descrption of Java Book",Category="Programming lang",Language="English",TotalPages=782},
                new BookModel(){Id=4,Title=".Net Core",Author="Akshay",Description="This is the descrption .Net Core MVC Book",Category="Framework",Language="English",TotalPages=1023},
                new BookModel(){Id=5,Title="SQL",Author="Vaibhav",Description="This is the descrption of SQL Book",Category="Programming",Language="English",TotalPages=1500},
                new BookModel(){Id=6,Title="VB .Net",Author="Rohit",Description="This is the descrption of VB .Net Book",Category=" Language",Language="English",TotalPages=725},
                new BookModel(){Id=7,Title="Javascript",Author="Rahul",Description="This is the descrption of Javascript Book",Category="Frontend",Language="English",TotalPages=965},


            };
        }
    }
}
