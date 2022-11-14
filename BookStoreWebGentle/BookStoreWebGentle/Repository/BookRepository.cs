using BookStoreWebGentle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public class BookRepository
    {
        
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
                new BookModel(){Id=1,Title="MVC",Author="Ashok"},
                new BookModel(){Id=2,Title="C#",Author="Deepak"},
                new BookModel(){Id=3,Title="Java",Author="Onkar"},
                new BookModel(){Id=4,Title=".Net Core",Author="Ashok"},
                new BookModel(){Id=5,Title="SQL",Author="Vaibhav"},
                new BookModel(){Id=6,Title="VB .Net",Author="Rohit"},
                new BookModel(){Id=7,Title="MySql",Author="Ashok"},
                new BookModel(){Id=8,Title="Javascript",Author="Rahul"},


            };
        }
    }
}
