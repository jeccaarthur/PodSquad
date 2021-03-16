using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PodSquad.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,60}$", ErrorMessage = "User's name must be alphabetic characters only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,60}$", ErrorMessage = "User's name must be alphabetic characters only")]
        public string LastName { get; set; }

        [NotMapped]
        public IList<String> RoleNames { get; set; }


        // TODO: add PodQueue property to store list of saved podcasts
    }
}
