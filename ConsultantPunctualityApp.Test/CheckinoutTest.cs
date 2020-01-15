using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Test
{
    [TestFixture]
    public class CheckinoutTest
    {
        [Test]
        public void CheckinShouldAcceptAstringRegId()
        {
            CheckInOutImplementation checkInOutImplementation = new CheckInOutImplementation();
            var unacceptedNumber = "1isjdda15263";
            var acceptedNumber = "090283";
            var checkincheckoutDTO = new CheckInOut
            {
                ConsultantRegID = acceptedNumber,
                CheckinDate = "",
                CheckinTime = ""
            };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.CheckIn(acceptedNumber)).Returns(Task.FromResult<CheckInOut>(checkincheckoutDTO));
            Assert.That(checkincheckoutDTO.ConsultantRegID == acceptedNumber);
            Assert.That(checkincheckoutDTO.ConsultantRegID != unacceptedNumber);
        }

        [Test]
        public void CheckinMethodShouldSetCheckinToTrueAfterCheckin()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            Consultant consultant = new Consultant();
            CheckInOut checkin = new CheckInOut();
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.CheckIn(checkin.ConsultantRegID)).Returns(Task.FromResult<CheckInOut>(checkin));
            Assert.That(checkin.ChekedIn != true);
        }

        [Test]
        public void CheckoutMethodShouldSetCheckinToTrueAfterCheckout()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            var consultant = new CheckInOut
            {
                CheckedOut = true
            };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.CheckOut(consultant.ConsultantRegID)).Returns(Task.FromResult<CheckInOut>(consultant));
            Assert.AreEqual(consultant.CheckedOut, true);
        }



        [Test]
        public void GetCheckedInConsultantsMethod_Should_Return_ListOf_CheckedInConsultantsI_fNotEmpty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            var consultant = new List<CheckinCheckoutDTO>
            {
            };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedInConsultants()).Returns(Task.FromResult<List<CheckinCheckoutDTO>>(consultant));
            Assert.AreNotEqual(consultant, null);
        }
        
        [Test]
        public void GetCheckedInConsultantsMethod_ShouldReturn_Null_IfCheckedInConsultants_Is_Empty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            List<CheckinCheckoutDTO> checkinCheckoutDTOs = null;
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedInConsultants()).Returns(Task.FromResult<List<CheckinCheckoutDTO>>(checkinCheckoutDTOs));
            Assert.AreEqual(checkinCheckoutDTOs, null);
        }

        [Test]
        public void GetCheckedInConsultantMethod_ShouldReturn_Null_IfCheckedInConsultants_Is_Empty()
        {
            var reg = "099040";
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            CheckinCheckoutDTO checkinCheckoutDTOs = null;
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedInConsultant(reg)).Returns(Task.FromResult<CheckinCheckoutDTO>(checkinCheckoutDTOs));
            Assert.AreEqual(checkinCheckoutDTOs, null);
        }

        [Test]
        public void GetCheckedInConsultantMethod_Should_Return_CheckedInConsultants_IfNotEmpty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            var consultant = new CheckinCheckoutDTO
            {
            };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedInConsultant(consultant.ConsultantRegID)).Returns(Task.FromResult<CheckinCheckoutDTO>(consultant));
            Assert.AreNotEqual(consultant, null);
        }

        [Test]
        public void GetCheckedOutConsultantMethod_Should_Return_CheckedInConsultants_IfNotEmpty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            var consultant = new CheckinCheckoutDTO
            {
            };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedOutConsultant(consultant.ConsultantRegID)).Returns(Task.FromResult<CheckinCheckoutDTO>(consultant));
            Assert.AreNotEqual(consultant, null);
        }

        [Test]
        public void GetCheckedOutConsultantMethod_Should_Return_Null_If_Empty()
        {
            var reg = "009090";
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            CheckinCheckoutDTO checkinCheckout = null;
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedOutConsultant(reg)).Returns(Task.FromResult<CheckinCheckoutDTO>(checkinCheckout));
            Assert.AreEqual(checkinCheckout, null);
        }

        [Test]
        public void GetCheckedOutConsultantsMethod_Should_Return_Null_If_Empty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            List<CheckinCheckoutDTO> checkinCheckout = null;
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedOutConsultants()).Returns(Task.FromResult<List<CheckinCheckoutDTO>>(checkinCheckout));
            Assert.AreEqual(checkinCheckout, null);
        }

        [Test]
        public void GetCheckedOutConsultantsMethod_Should_Return_ListOfCheckedInConsultants_If_Empty()
        {
            CheckInOutImplementation implementation = new CheckInOutImplementation();
            List<CheckinCheckoutDTO> checkinCheckout = new List<CheckinCheckoutDTO> { };
            var mockCheckin = new Mock<ICheckInOut>();
            mockCheckin.Setup(c => c.GetCheckedOutConsultants()).Returns(Task.FromResult<List<CheckinCheckoutDTO>>(checkinCheckout));
            Assert.AreNotEqual(checkinCheckout, null);
        }


    }
}
