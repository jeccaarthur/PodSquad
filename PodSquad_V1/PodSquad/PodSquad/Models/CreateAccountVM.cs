using System;
using System.ComponentModel.DataAnnotations;

namespace PodSquad.Models
{
    public class CreateAccountVM
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z0-9_]{1,60}$", ErrorMessage = "Username can contain alphanumeric characters and underscores only")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
