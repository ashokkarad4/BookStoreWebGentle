using BookStoreWebGentle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<int> DeleteBook(int? id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<BookModel> SearchBook(string title, string authorName);

        string GetAppName();
     }
}