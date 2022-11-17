using BookStoreWebGentle.Data;
using BookStoreWebGentle.Models;
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
        public int AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                //BookRepository 
            };
        }
        
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
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
