﻿using System;
namespace PodSquad.Models
{
    public class Comment
    {
        // TODO: uncomment properties when other models have been built

        public int CommentID { get; set; }
        //public AppUser Commenter { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
    }
}