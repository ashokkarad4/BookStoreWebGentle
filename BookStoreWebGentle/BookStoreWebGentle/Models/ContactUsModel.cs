using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Models
{
    public class ContactUsModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please Enter the FirstName")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter the LastName")]

        public string LastName { get; set; }
        [Required(ErrorMessage ="Please Enter the Email")]


        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter the Phone number")]
        public string Phone
        {
            get; set;
        }
        [Required(ErrorMessage = "Please Enter Query")]
        public string Description
        {
            get; set;
        }

    }
}
