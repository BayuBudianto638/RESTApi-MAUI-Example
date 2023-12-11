﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.UserService.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NoHP { get; set; }
        public string PasswordSalt { get; set; }
    }
}
