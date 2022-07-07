using System.ComponentModel.DataAnnotations;


namespace ServiceLayer.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "An email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "A city name is required")]

        public string City { get; set; }

        [Required(ErrorMessage = "A country name is required")]
        public string Country { get; set; }

    }
}
