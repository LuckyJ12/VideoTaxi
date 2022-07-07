using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ServiceLayer.Models
{
    public class Login
    {
        [Required(ErrorMessage = "An email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
    }
}
