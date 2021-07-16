using System;
using System.Net.Http;

using Fincon_Assessment_WebAPI.Controllers;
using Fincon_Assessment_WebAPI.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fincon_Assessment_Test
{
    [TestClass]
    public class RegisterTest
    {
        [TestMethod]
        public void is_valid_Register()
        {
            var controller = new RegisterController();
            Register _Register = new Register();
            _Register.Name = "hardik990@yahoo.com";
            _Register.Password = "1234567890";
            var result = controller.Register(_Register) as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }
    }
}

