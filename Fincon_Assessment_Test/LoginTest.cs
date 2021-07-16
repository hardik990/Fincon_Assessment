using System;
using System.Net.Http;

using Fincon_Assessment_WebAPI.Controllers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fincon_Assessment_Test
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void is_valid_Login()
        {
            var controller = new LoginController();
            var result = controller.Login("hardik990@yahoo.com", "1234567890") as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }
    }
}
