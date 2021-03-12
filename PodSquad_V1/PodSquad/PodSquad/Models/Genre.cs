using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodSquad.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
