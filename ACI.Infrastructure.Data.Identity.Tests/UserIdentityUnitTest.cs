using ACI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace ACI.Infrastructure.Data.Identity.Tests
{
    [TestClass]
    public class UserIdentityUnitTest
    {


        public static IConfiguration Configuration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        [TestMethod]
        public void Create()
        {
            UserIdentity userIdentity = new UserIdentity(Configuration());

            UserEntity user = new UserEntity(
               Guid.NewGuid(),
               "José",
               "Balmaceda",
               "sample@sample.com",
               "sample@sample.com".ToUpper(),
               true,
               "?",
               Convert.ToDateTime("1986-08-20"),
               DateTime.Now);

            var result = userIdentity.CreateAsync(user, new CancellationToken()).Result;
            Assert.IsNotNull(result, "error insert");
        }

        [TestMethod]
        public void FindByEmail()
        {
            UserIdentity userIdentity = new UserIdentity(Configuration());
            var result = userIdentity.FindByEmailAsync("sample@sample.com", new CancellationToken()).Result;
            Assert.IsNotNull(result, "user is null");
        }
    }
}
