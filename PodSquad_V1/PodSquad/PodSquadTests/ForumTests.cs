using NUnit.Framework;
using System.Linq;
using PodSquad.Models;
using PodSquad.Repositories;
using PodSquad.Controllers;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PodSquadTests
{
    public class ForumTests
    {
        FakeForumRepository fakeRepo;
        ForumController controller;
        UserManager<AppUser> userManager;

        List<Post> posts;
        List<Reply> replies;

        Post post1;
        Post post2;
        Post post3;
        Post post4;

        Reply reply1;
        Reply reply2;
        Reply reply3;
        Reply reply4;

        AppUser user1;
        AppUser user2;

        [SetUp]
        public void Setup()
        {
            fakeRepo = new FakeForumRepository();
            controller = new ForumController(fakeRepo, userManager);

            user1 = new AppUser
            {
                UserName = "TestUser 1",
                FirstName = "First 1",
                LastName = "Last 1"
            };

            user2 = new AppUser
            {
                UserName = "TestUser 2",
                FirstName = "First 2",
                LastName = "Last 2"
            };

            post1 = new Post
            {
                Title = "Post Title 1",
                Body = "Post body 1",
                Date = DateTime.Now,
                Poster = user1
            };

            post2 = new Post
            {
                Title = "Post Title 2",
                Body = "Post body 2",
                Date = DateTime.Now,
                Poster = user1
            };

            post3 = new Post
            {
                Title = "Post Title 3",
                Body = "Post body 3",
                Date = DateTime.Now,
                Poster = user2
            };

            post4 = new Post
            {
                Title = "Post Title 4",
                Body = "Post body 4",
                Date = DateTime.Now,
                Poster = user2
            };

            reply1 = new Reply
            {
                Responder = user2,
                Date = DateTime.Now,
                ReplyText = "Reply text 1"
            };

            reply2 = new Reply
            {
                Responder = user2,
                Date = DateTime.Now,
                ReplyText = "Reply text 2"
            };

            reply3 = new Reply
            {
                Responder = user1,
                Date = DateTime.Now,
                ReplyText = "Reply text 3"
            };

            reply4 = new Reply
            {
                Responder = user1,
                Date = DateTime.Now,
                ReplyText = "Reply text 4"
            };
        }


        #region POST TESTS

        [Test]
        public void TestAddPost()
        {
            // use controller method to add post to repo
            controller.Post(post1);

            // retrieve post from repo
            Post post = fakeRepo.Posts.First(p => p.PostID == post1.PostID);

            // check values
            Assert.IsNotNull(post);
            Assert.AreEqual(0, post.PostID);
            Assert.AreEqual("Post Title 1", post.Title);
            Assert.AreEqual("Post body 1", post.Body);
            Assert.AreEqual(user1, post.Poster);
        }

        [Test]
        public void TestGetPostByID()
        {
            // add posts to repo
            controller.Post(post1);
            controller.Post(post2);
            controller.Post(post3);

            // retrieve post from repo using id
            Post post = fakeRepo.Posts.First(p => p.PostID == post2.PostID);

            // check values
            Assert.IsNotNull(post);
            Assert.AreEqual(1, post.PostID);
            Assert.AreEqual("Post Title 2", post.Title);
            Assert.AreEqual("Post body 2", post.Body);
            Assert.AreEqual(user1, post.Poster);
        }

        [Test]
        public void TestGetAllPosts()
        {
            // add posts to repo
            controller.Post(post1);
            controller.Post(post2);
            controller.Post(post3);
            controller.Post(post4);

            // retrieve all posts
            posts = fakeRepo.GetAllPosts();

            // check count
            Assert.AreEqual(4, posts.Count);
        }

        #endregion





        #region REPLY TESTS

        [Test]
        public void TestAddReply()
        {
            // add post to repo and retrieve it again
            controller.Post(post1);
            Post post = fakeRepo.GetPostByID(post1.PostID);

            // confirm post has no replies associated to it
            Assert.IsTrue(post.Replies.Count == 0);

            // create a replyVM object and assign the postID to it
            ReplyVM replyVM = new ReplyVM();
            replyVM.Post = post;
            replyVM.ReplyText = reply1.ReplyText;

            // call controller method to add a reply and pass replyVM to it
            controller.Reply(replyVM);

            // confirm reply was saved to the post
            Post updatedPost = fakeRepo.GetPostByID(post.PostID);
            Assert.IsTrue(updatedPost.Replies.Count == 1);
            Assert.AreEqual(replyVM.ReplyText, reply1.ReplyText);
        }


        #endregion
    }
}
