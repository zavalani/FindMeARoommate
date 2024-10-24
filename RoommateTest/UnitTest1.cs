using FIndMeARoommate;
using FIndMeARoommate.Controllers;
using FIndMeARoommate.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using Microsoft.Extensions.Options;

namespace RoommateTest
{
    public class Tests
    {
        private RoommateDbContext _context;



        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<RoommateDbContext>()
                .Options;
            _context = new RoommateDbContext(options);
            Announcements ann1 = new Announcements { Id = 1, Title = "Test Announcement 1", Description = "Description 1", PublishedDate = System.DateTime.Now, IsActive = true };
            Announcements ann2 = new Announcements { Id = 2, Title = "Test Announcement 2", Description = "Description 2", PublishedDate = System.DateTime.Now, IsActive = false };
            var controller = new AnnouncementsController(_context);
            var result1 = controller.AnnouncementsExists(3);
            if (result1 == false)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var options = new DbContextOptionsBuilder<RoommateDbContext>()
                .Options;
            _context = new RoommateDbContext(options);
            Announcements ann1 = new Announcements { Id = 1, Title = "Test Announcement 1", Description = "Description 1", PublishedDate = System.DateTime.Now, IsActive = true };
            Announcements ann2 = new Announcements { Id = 2, Title = "Test Announcement 2", Description = "Description 2", PublishedDate = System.DateTime.Now, IsActive = false };
            var controller = new AnnouncementsController(_context);
            var result1 = controller.AnnouncementsExists(1);
            if (result1 == false)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
}
