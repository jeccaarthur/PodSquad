using System;
using System.Collections.Generic;
using System.Linq;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public class FakeForumRepository : IForumRepository
    {
        private List<Post> posts = new List<Post>();
        private List<Reply> replies = new List<Reply>();


        #region POST METHODS

        public IQueryable<Post> Posts
        {
            get
            {
                return posts.AsQueryable<Post>();
            }
        }

        // add a new post
        public void AddPost(Post post)
        {
            // simulate auto-incremented primary key and add post to list
            post.PostID = posts.Count;
            posts.Add(post);
        }

        // retrieve all posts
        public List<Post> GetAllPosts()
        {
            posts = Posts.OrderByDescending(p => p.Date).ToList();

            return posts;
        }

        // retrieve post with matching id
        public Post GetPostByID(int id)
        {
            Post post = posts.Find(p => p.PostID == id);
            return post;
        }

        



        #endregion






        #region REPLY METHODS

        public IQueryable<Reply> Replies
        {
            get
            {
                return replies.AsQueryable<Reply>();
            }
        }

        // adds a new reply
        public void AddReply(Reply reply)
        {
            replies.Add(reply);
        }

        // retrieve reply with matching id
        public Reply GetReplyByID(int id)
        {
            Reply reply = replies.Find(r => r.ReplyID == id);
            return reply;
        }



        #endregion
    }
}
