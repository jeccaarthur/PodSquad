using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public class ForumRepository : IForumRepository
    {
        private PodContext context;

        public ForumRepository(PodContext c)
        {
            context = c;
        }
    }
}
