﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Models
{
    public class ManageUserRolesViewModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
