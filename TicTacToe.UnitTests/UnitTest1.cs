using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Controllers;
using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Contact_AsViewResult_ReturnTrue()
        {
            HomeController controller = new HomeController();
            ViewResult contact = controller.Contact() as ViewResult;
            NUnit.Framework.Assert.IsInstanceOf(typeof(ViewResult),contact);
        }

        [TestMethod]
        public void Contact_ReturnsRightData_ReturnsTrue()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Contact() as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            NUnit.Framework.Assert.AreEqual("Strona kontaktowa", viewData["Message"]);
        }

    
        [TestMethod]
        public void Contact_InstanceOfType_ReturnTrue()
        {
            HomeController homeController = new HomeController();
            ActionResult result = homeController.Contact();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
