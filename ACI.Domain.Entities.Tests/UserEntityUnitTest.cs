using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ACI.Domain.Entities.Tests
{
    [TestClass]
    public class UserEntityUnitTest
    {
        [TestMethod]
        public void HasMinimunAge()
        {
            UserEntity user = new UserEntity(
                Guid.NewGuid(),
                "José",
                "Balmaceda",
                "sample@sample.com",
                "sample@sample.com".ToUpper(),
                true,
                "?",
                Convert.ToDateTime("2002-08-20"),
                DateTime.Now);

            var result = user.HasMinimunAge();

            Assert.IsTrue(result, "error, don't has minimum age");
        }
    }
}
