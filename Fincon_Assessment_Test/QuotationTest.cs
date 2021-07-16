using System;
using System.Net.Http;

using Fincon_Assessment_WebAPI.Controllers;
using Fincon_Assessment_WebAPI.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fincon_Assessment_Test
{
    [TestClass]
    public class QuotationTest
    {
        [TestMethod]
        public void can_add_Quotation()
        {
            var controller = new QuotationController();
            Quotation _Quotation = new Quotation();
            var result = controller.addQuotation(_Quotation) as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void can_viewAll_Quotation()
        {
            var controller = new QuotationController();
            var result = controller.viewAllQuotation("1") as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void can_view_Quotation()
        {
            var controller = new QuotationController();
            var result = controller.viewQuotation("1") as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void can_Delete_Quotation()
        {
            var controller = new QuotationController();
            var result = controller.DeleteQuotation("1", "1") as HttpResponseMessage;
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }
    }
}
