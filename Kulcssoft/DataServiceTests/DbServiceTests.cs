using NUnit.Framework;
using System.Data;

namespace DataService.Tests
{
    [TestFixture]
    public class DbServiceTests
    {
        DbService service;
        string name = "Jacob";
        string email = "mail@maily.com";

        [SetUp]
        public void SetUp()
        {
            service = new DbService(new MemDb());   // I'm sorry if it's not nice, but due to lack of time I couldn't find any better solution
            service.AddUser(name, email);
        }
        
        [Test]
        public void DeletUserTest()
        {
            int id = 0;
            int oldUserCount = service.GetUsers().Rows.Count;
            service.DeletUser(id);
            int newUserCount = service.GetUsers().Rows.Count;
            Assert.AreNotEqual(oldUserCount, newUserCount);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void GetUserTest_WithValidId(int id)
        {
            Assert.IsNotNull(service.GetUser(id));
        }
        [TestCase(-1)]
        [TestCase(999999999)]
        public void GetUserTest_WithFalseId(int id)
        {
            Assert.IsNull(service.GetUser(id));
        }

        [TestCase(null)]
        [TestCase("email")]
        public void GetUserTest_WithFalseEmail(string email)
        {
            Assert.IsNull(service.GetUser(email));
        }

        [Test]
        public void GetUserTest_WithValidEmail()
        {
            Assert.IsNotNull(service.GetUser(email));
        }
        [Test]
        public void GetUsersTest()
        {
            Assert.Greater(service.GetUsers().Rows.Count, 0);
        }

        [Test]
        public void InsertUserTest()
        {
            int oldUserCount = service.GetUsers().Rows.Count;
            service.AddUser(name, email);
            int newUserCount = service.GetUsers().Rows.Count;
            Assert.AreNotEqual(oldUserCount, newUserCount);
        }
    }
}