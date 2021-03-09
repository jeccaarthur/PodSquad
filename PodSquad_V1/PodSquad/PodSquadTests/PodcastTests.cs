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
    public class PodcastTests
    {
        FakePodcastRepository fakeRepo;
        PodcastController controller;
        UserManager<AppUser> userManager;

        List<Podcast> podcasts;
        List<Genre> genres;
        List<Review> reviews;

        Podcast pod1;
        Podcast pod2;
        Podcast pod3;
        Podcast pod4;

        Review review1;
        Review review2;

        AppUser user1;
        AppUser user2;


        [SetUp]
        public void Setup()
        {
            fakeRepo = new FakePodcastRepository();
            controller = new PodcastController(fakeRepo, userManager);

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

            pod1 = new Podcast
            {
                Name = "Test Podcast",
                Genre = new Genre
                {
                    Name = "Genre 1",
                    Description = "Genre description 1"
                },
                Network = "Test Network",
                HostName = "Test Host",
                Description = "Podcast description 1"
            };

            pod2 = new Podcast
            {
                Name = "Test Podcast 2",
                Genre = new Genre
                {
                    Name = "Genre 2",
                    Description = "Genre description 2"
                },
                Network = "Test Network 2",
                HostName = "Test Host 2",
                Description = "Podcast description 2"
            };

            pod3 = new Podcast
            {
                Name = "Test Podcast 3",
                Genre = new Genre
                {
                    Name = "Genre 1",
                    Description = "Genre description 1"
                },
                Network = "Test Network 3",
                HostName = "Test Host 3",
                Description = "Podcast description 3"
            };

            pod4 = new Podcast
            {
                Name = "Test Podcast 4",
                Genre = new Genre
                {
                    Name = "Genre 2",
                    Description = "Genre description 2"
                },
                Network = "Duplicate Test Nework",
                HostName = "Duplicate Test Host",
                Description = "Duplicate test description"
            };

            review1 = new Review
            {
                Date = DateTime.Now,
                Rating = 4,
                ReviewText = "Review description 1"
            };

            review2 = new Review
            {
                Date = DateTime.Now,
                Rating = 5,
                ReviewText = "Review description 2"
            };
        }



        #region PODCAST TESTS


        [Test]
        public void TestAddPod()
        {
            // use controller method to add podcast to repo
            controller.AddPod(pod1);

            // retrieve podcast from repo
            Podcast pod = fakeRepo.Podcasts.ToList()[0];

            // check values
            Assert.IsNotNull(pod);
            Assert.AreEqual(0, pod.PodcastID);
            Assert.AreEqual("Test Podcast", pod.Name);
            Assert.AreEqual("Genre 1", pod.Genre.Name);
            Assert.AreEqual("Test Network", pod.Network);
            Assert.AreEqual("Test Host", pod.HostName);
            Assert.AreEqual("Podcast description 1", pod.Description);
        }

        [Test]      // should not be able to add pods with same name
        public void TestAddDuplicatePod()
        {
            // use controller method to add podcast to repo
            controller.AddPod(pod3);

            // retrieve podcast from repo and confirm name
            Podcast pod = fakeRepo.Podcasts.First(p => p.PodcastID == pod3.PodcastID);
            Assert.IsNotNull(pod);
            Assert.AreEqual(pod3.Name, pod.Name);

            // try to add a podcast with the same name - this should fail
            try
            {
                controller.AddPod(pod4);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Podcast already exists"));
            }
        }

        [Test]
        public void TestGetPodByID()
        {
            // add podcasts to repo
            controller.AddPod(pod1);
            controller.AddPod(pod2);

            // retrieve podcast from repo using id
            Podcast pod = fakeRepo.Podcasts.First(p => p.PodcastID == pod1.PodcastID);

            // check values
            Assert.IsNotNull(pod);
            Assert.AreEqual(pod1.PodcastID, pod.PodcastID);
            Assert.AreEqual(pod1.Name, pod.Name);
            Assert.AreEqual(pod1.Network, pod.Network);
            Assert.AreEqual(pod1.HostName, pod.HostName);
            Assert.AreEqual(pod1.Description, pod.Description);
        }

        [Test]
        public void TestGetPodByName()
        {
            // add podcasts to repo
            controller.AddPod(pod1);
            controller.AddPod(pod2);

            // retrieve podcast from repo using id
            Podcast pod = fakeRepo.Podcasts.First(p => p.Name == pod1.Name);

            // check values
            Assert.IsNotNull(pod);
            Assert.AreEqual(pod1.PodcastID, pod.PodcastID);
            Assert.AreEqual(pod1.Name, pod.Name);
            Assert.AreEqual(pod1.Network, pod.Network);
            Assert.AreEqual(pod1.HostName, pod.HostName);
            Assert.AreEqual(pod1.Description, pod.Description);
        }

        [Test]
        public void TestAddReview()
        {
            // add podcast to repo and retrieve it to get its id
            controller.AddPod(pod1);
            Podcast pod = fakeRepo.Podcasts.First(p => p.Name == pod1.Name);

            // confirm pod has no reviews associated to it
            Assert.IsTrue(pod.Reviews.Count == 0);

            // create a reviewVM object and assign the podcast id to it, and assign other review values
            ReviewVM reviewVM = new ReviewVM();
            reviewVM.PodcastID = pod.PodcastID;
            reviewVM.Rating = review1.Rating;
            reviewVM.ReviewText = review1.ReviewText;

            // call controller method to add a review and pass reviewVM to it
            controller.Review(reviewVM);

            // confirm review was saved to the podcast
            Podcast updatedPod = fakeRepo.GetPodByID(pod.PodcastID);
            Assert.IsTrue(updatedPod.Reviews.Count == 1);
            Assert.AreEqual(reviewVM.Rating, updatedPod.Reviews[0].Rating);
            Assert.AreEqual(reviewVM.ReviewText, updatedPod.Reviews[0].ReviewText);





        }

        #endregion



        #region GENRE TESTS

        [Test]
        public void TestAddGenre()
        {
            // use controller method to add podcast to repo
            controller.AddPod(pod1);

            // retrieve genre from repo
            Genre genre = fakeRepo.Genres.ToList()[0];

            // check values
            Assert.IsNotNull(genre);
            Assert.AreEqual(0, genre.GenreID);
            Assert.AreEqual("Genre 1", genre.Name);

            // retrieve podcast that was saved to db
            Podcast pod = fakeRepo.GetPodByID(pod1.PodcastID);

            // confirm genre saved to podcast properly
            Assert.AreEqual(0, pod.Genre.GenreID);
            Assert.AreEqual(pod1.Genre.Name, pod.Genre.Name);
        }

        [Test]      // should not add duplicate rows
        public void TestAddDuplicateGenre()
        {
            // use controller method to add podcasts with same genres
            controller.AddPod(pod1);    // genre 1
            controller.AddPod(pod2);    // genre 2
            controller.AddPod(pod3);    // genre 1
            controller.AddPod(pod4);    // genre 2

            // retrieve list of genres from repo
            genres = fakeRepo.Genres.ToList();

            // count total number of genres - should be 2
            Assert.AreEqual(2, genres.Count);
            Assert.AreEqual("Genre 1", genres[0].Name);
            Assert.AreEqual("Genre 2", genres[1].Name);
        }

        [Test]
        public void TestGetPodsByGenre()
        {
            // add podcasts to repo
            controller.AddPod(pod1);    // genre 1
            controller.AddPod(pod2);    // genre 2
            controller.AddPod(pod3);    // genre 1
            controller.AddPod(pod4);    // genre 2

            // retrieve podcasts with genre id 0 and count - should be 2
            podcasts = fakeRepo.GetPodsByGenre(0);
            Assert.AreEqual(2, podcasts.Count);

            // clear list
            podcasts.Clear();

            // retrieve podcasts with genre id 1 and count - should be 2
            podcasts = fakeRepo.GetPodsByGenre(1);
            Assert.AreEqual(2, podcasts.Count);
        }


        #endregion
    }
}