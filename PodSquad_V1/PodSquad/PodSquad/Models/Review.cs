using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PodSquad.Models;

namespace PodSquad.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public AppUser Reviewer { get; set; }
        public DateTime Date { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(350)]
        public string ReviewText { get; set; }
    }
}
