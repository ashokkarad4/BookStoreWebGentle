using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStoreWebGentle.Helper;

namespace BookStoreWebGentle.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please Enter the Title of your book")]

      //  [MyCustomValidationAttribute("azure")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please Enter the Author Of Book")]

        public string Author { get; set; }
        [StringLength(500, MinimumLength = 10)]

        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }

        [Required(ErrorMessage ="Please Enter the Pages of Book")]
        [Display(Name ="Total Pages of Book")]

        public int? TotalPages { get; set; }
        [Required(ErrorMessage = "Please Enter the Price of Book")]
        public double? Price { get; set; }
       

    }
}
