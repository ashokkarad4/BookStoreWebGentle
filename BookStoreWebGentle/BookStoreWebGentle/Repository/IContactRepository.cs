using BookStoreWebGentle.Models;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public interface IContactRepository
    {
        Task<int> AddNewContact(ContactUsModel model);
    }
}