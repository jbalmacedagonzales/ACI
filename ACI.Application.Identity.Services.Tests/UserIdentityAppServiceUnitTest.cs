using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACI.Application.Identity.Services;
using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using ACI.Infrastructure.Data.Identity;
using ACI.Application.Identity.DTOs;
using System.Threading;
using System;

namespace ACI.Application.Identity.Services.Tests
{
    [TestClass]
    public class UserIdentityAppServiceUnitTest
    {

        public static IConfiguration Configuration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        [TestMethod]
        public void Create()
        {
            IUserIdentity userIdentity = new UserIdentity(Configuration());
            UserIdentityAppService userIdentityAppService = new UserIdentityAppService(userIdentity);

            var userDTO = new UserDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "use02",
                LastName = "some lastname",
                Email = "sample2@sample.com",
                NormalizedEmail = "sample2@sample.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = "?",
                BirthDate = Convert.ToDateTime("1980-08-20"),
                RegistrationDate = DateTime.Now
            };

            var result = userIdentityAppService.CreateAsync(userDTO, new CancellationToken()).Result;
            Assert.IsTrue(result.Succeeded, "error");
        }
    }
}
