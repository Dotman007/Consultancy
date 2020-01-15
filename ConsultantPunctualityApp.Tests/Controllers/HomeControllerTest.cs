using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsultantPunctualityApp;
using ConsultantPunctualityApp.Controllers;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.DAL;

namespace ConsultantPunctualityApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly ConsultantDB _consultantdb;
        public HomeControllerTest(ConsultantDB consultantdb)
        {
            _consultantdb = consultantdb;
        }
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void CheckConsultant()
        {
            ConsultantImplementation consultantImplementation = new ConsultantImplementation(_consultantdb);
            var CheckConsultant = consultantImplementation.AddConsultant(new Models.Consultant());
            Assert.IsNotNull(CheckConsultant);
        }
    }
}
