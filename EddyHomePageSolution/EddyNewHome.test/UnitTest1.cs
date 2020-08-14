using System;
using System.Security.AccessControl;
using EddyNewHome.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EddyNewHome.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string exip = "210.119.12.66";
            string actIp = Commons.GetExteralIp();
            Assert.AreEqual(exip, actIp);
        }
    }
}
