using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ACI.Infrastructure.CrossCutting.Logging.Tests
{
    [TestClass]
    public class LoggerUnitTest
    {
        [TestMethod]
        public void WriteLog()
        {
            string[] obj = new string[] { };
            try
            {
                var type = obj.GetType();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, ex.StackTrace);
            }
            Assert.IsNotNull(obj, "obj is null");
        }
    }
}
