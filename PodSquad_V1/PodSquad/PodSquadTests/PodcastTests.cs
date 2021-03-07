using NUnit.Framework;
using System.Linq;
using PodSquad.Models;
using PodSquad.Repositories;
using PodSquad.Controllers;
using System;


namespace PodSquadTests
{
    public class PodcastTests
    {
        FakePodcastRepository fakeRepo;
        PodcastController controller;

        Podcast pod1;
        Podcast pod2;

        Review review1;
        Review review2;

        AppUser user1;
        AppUser user2;


        [SetUp]
        public void Setup()
        {
            fakeRepo = new FakePodcastRepository();
            controller = new PodcastController(fakeRepo);

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
                Genre =
                {
                //    GenreID = 1,
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
                Genre =
                {
                //    GenreID = 2,
                    Name = "Genre 2",
                    Description = "Genre description 1"
                },
                Network = "Test Network 2",
                HostName = "Test Host 2",
                Description = "Podcast description 2"
            };

            pod3 = new Podcast
            {
                Name = "Test Podcast 2",
                Genre =
                {
                //    GenreID = 2,
                    Name = "Genre 1",
                    Description = "Genre description 1"
                },
                Network = "Test Network 2",
                HostName = "Test Host 2",
                Description = "Podcast description 2"
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
            Assert.AreEqual("Test Network", pod.Network);
            Assert.AreEqual("Test Host", pod.HostName);
            Assert.AreEqual("Podcast description 1", pod.Description);
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
    }
}