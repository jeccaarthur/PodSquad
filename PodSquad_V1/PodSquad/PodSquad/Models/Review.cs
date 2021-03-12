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
        // TODO: add validation to fields

        //private List<Comment> comments = new List<Comment>();

        public int ReviewID { get; set; }
        public AppUser Reviewer { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        //public List<Comment> Comments { get { return comments; } }
    }
}
