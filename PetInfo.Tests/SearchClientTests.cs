using System.Linq;
using System.Threading;
using NUnit.Framework;
using PetInfo.Web.models;
using PetInfo.Web.Search;

namespace PetInfo.Tests
{
    [TestFixture]
    public class SearchClientTests
    {
        private SearchClient client;

        [SetUp]
        public void Setup()
        {
            client = new SearchClient();
            client.Index(new Owner { Name = "marten olofsson", Age = 22, Id = 1 });
            client.Index(new Owner { Name = "olofsson jolle", Age = 33, Id = 2 });
            Thread.Sleep(3000);
        }
        [Test]
        public void ShouldReturnDocuments()
        {
            var result = client.Search("marten");
            Assert.That(result.Result.Documents.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldReturnDocuments2()
        {
           

            var result = client.Search("olofsson");
            Assert.That(result.Result.Documents.Count(), Is.EqualTo(2));
        }

        [Ignore]
        [Test]
        public void ShouldReturnDocuments_WhenUsingAgeInSearch()
        {
            client.Index(new Owner { Name = "marten olofsson", Age = 22 });
            client.Index(new Owner { Name = "olofsson jolle", Age = 33 });

            var result = client.Search("33");
            Assert.That(result.Result.Documents.Count(), Is.EqualTo(1));
            
        }

        [TearDown]
        public void Teardown()
        {
            client.Delete();
        }
    }
}
