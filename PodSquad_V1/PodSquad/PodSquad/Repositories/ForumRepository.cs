using System;
using System.Collections.Generic;
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



        #region POST METHODS

        public IQueryable<Post> Posts
        {
            get
            {
                return context.Posts
                    .Include(post => post.Poster)
                    .Include(post => post.Replies)
                    .ThenInclude(reply => reply.Responder);
            }
        }

        // add a new post
        public void AddPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        // retrieve all posts
        public List<Post> GetAllPosts()
        {
            List<Post> posts = context.Posts.OrderByDescending(p => p.Date)
                .Include(p => p.Poster)
                .Include(p => p.Replies)
                .ThenInclude(r => r.Responder).ToList();

            return posts;
        }

        // retrieve post with matching id
        public Post GetPostByID(int id)
        {
            Post post = context.Posts.Where(p => p.PostID == id)
                .Include(p => p.Poster)
                .Include(p => p.Replies)
                .ThenInclude(r => r.Responder).FirstOrDefault();

            return post;
        }

        

        #endregion






        #region REPLY METHODS

        public IQueryable<Reply> Replies
        {
            get
            {
                return context.Replies.Include(reply => reply.Responder);
            }
        }

        // adds a new reply
        public void AddReply(Reply reply)
        {
            // save reply to db
            context.Replies.Add(reply);
            context.SaveChanges();
        }

        // retrieve reply with matching id
        public Reply GetReplyByID(int id)
        {
            Reply reply = context.Replies
                .Include(reply => reply.Responder)
                .Where(r => r.ReplyID == id).FirstOrDefault();

            return reply;
        }

        #endregion
    }
}
