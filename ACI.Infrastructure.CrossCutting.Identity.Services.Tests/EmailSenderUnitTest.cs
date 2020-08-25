using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ACI.Infrastructure.CrossCutting.Identity.Services.Tests
{
    [TestClass]
    public class EmailSenderUnitTest
    {
        [TestMethod]
        public void SendEmail()
        {
            EmailSender emailSender = new EmailSender();
            var result = emailSender.SendAsync("josebalmacedape@outlook.es", "This is a test...").Result;
            Assert.IsTrue(result, "error in send email");
        }
    }
}
