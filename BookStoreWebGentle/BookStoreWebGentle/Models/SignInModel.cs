﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Models
{
    public class SignInModel
    {
        [Required,EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]

        public bool RememberMe { get; set; }

    }
}
