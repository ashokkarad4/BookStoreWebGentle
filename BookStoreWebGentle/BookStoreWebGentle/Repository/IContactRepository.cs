using BookStoreWebGentle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public interface IContactRepository
    {
        Task<int> AddNewContact(ContactUsModel model);
        Task<List<ContactUsModel>> GetAllContacts();
     }
}