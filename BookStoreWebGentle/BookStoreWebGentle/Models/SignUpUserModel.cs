using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Models
{
    public class SignUpUserModel
    {

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
      
        public DateTime? DateOfBirth { get; set; }


        [Required(ErrorMessage ="Please Enter Your Email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Please enter a Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Strong Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "Please Confirm your Password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match")]

        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

    }
}
