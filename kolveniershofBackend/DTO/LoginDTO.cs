﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class LoginDTO
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }
    }
}
