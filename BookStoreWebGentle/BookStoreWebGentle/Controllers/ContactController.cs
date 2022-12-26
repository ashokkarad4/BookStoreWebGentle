using BookStoreWebGentle.Models;
using BookStoreWebGentle.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
      
        public ContactController(IContactRepository contactRepository, IWebHostEnvironment webHostEnvironment)
        {
            _contactRepository = contactRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public ViewResult AddnewContact(bool isSuccess=false,int contactId=0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.ContactId = contactId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewContact(ContactUsModel contactModel)
        {
            int id =await _contactRepository.AddNewContact(contactModel);
            if (id>0)
            {
                return RedirectToAction(nameof(AddnewContact), new { isSuccess = true, bookId = id });
            }
            return View();
        }

        [Route("all-contacts")]
        public async Task<ViewResult> GetAllContacts()
        {
            var data = await _contactRepository.GetAllContacts();

            return View(data);
        }

    }
}
