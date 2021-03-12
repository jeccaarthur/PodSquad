using System;
using System.Collections.Generic;
using System.Linq;
using PodSquad.Models;

namespace PodSquad.Repositories
{
    public interface IForumRepository
    {
        // post methods
        IQueryable<Post> Posts { get; }
        void AddPost(Post post);
        List<Post> GetAllPosts();
        Post GetPostByID(int id);
        //void EditPost(Post post);
        //void DeletePost(int id);

        // reply methods
        IQueryable<Reply> Replies { get; }
        void AddReply(Reply reply);
        //List<Reply> GetAllReplies(int postID);
        Reply GetReplyByID(int id);
        //void EditReply(Reply reply);
        //void DeleteReply(int id);
    }
}
