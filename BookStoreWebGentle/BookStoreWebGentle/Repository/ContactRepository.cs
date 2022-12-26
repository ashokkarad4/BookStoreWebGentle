using BookStoreWebGentle.Data;
using BookStoreWebGentle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public class ContactRepository:IContactRepository
    {

        private readonly BookStoreContext _context = null;
        public ContactRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewContact(ContactUsModel model)
        {

            var newContact = new ContactsData()
            {
                //BookRepository
                
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone

                };
            await _context.Contacts.AddAsync(newContact);
           await _context.SaveChangesAsync();
               

                return newContact.Id;
            
           
        }

     



        
    }
}
