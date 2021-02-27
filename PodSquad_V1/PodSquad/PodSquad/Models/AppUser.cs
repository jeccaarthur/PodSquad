using System;
using System.Collections.Generic;

namespace PodSquad.Models
{
    public class AppUser
    {
        // TODO: inherit from identity user class after adding identity service

        public string Name { get; set; }
        public List<String> RoleNames { get; set; }
    }
}
