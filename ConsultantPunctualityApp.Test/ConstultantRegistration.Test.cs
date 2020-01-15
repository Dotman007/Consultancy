using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Moq;
using NUnit.Framework;
namespace ConsultantPunctualityApp.Test
{
    public class ConsultantRegistrationImplementationTest
    {
        private readonly ConsultantDB _consultantdb = new ConsultantDB();
       
        [Test]
        public void GenerateRegistrationId_Method_Should_Return_String_OfNumbers()
        {
            ConsultantImplementation implementation = new ConsultantImplementation();
            var result = implementation.GenerateRegID();
            Assert.That(result, !Is.NaN);
        }

        [Test]
        public void GenerateRegistrationId_Method_Should_Return_String_That_IsNot_Greater_Than_Or_Lessthan_Six()
        {
            ConsultantImplementation implementation = new ConsultantImplementation();
            var result = implementation.GenerateRegID();
            Assert.That(!(result.Length > 6) && !(result.Length < 6));
            Assert.That(result.Length == 6);
        }
        
        [Test]
        public void GenerateRegistrationId_Method_Should_Return_String_That_DoesNotContain_Letters()
        {
            ConsultantImplementation implementation = new ConsultantImplementation();
            var result = implementation.GenerateRegID();
            var letter = "absjdkdkdsu";
            var doesntContainletters = !(result.Contains(letter));
            Assert.That(doesntContainletters);
        }


        [Test]
        public void GetConsultant_Should_Return_Consultant_Object()
        {

            //Arrange 
            var consultant = new Mock<IConsultant>(MockBehavior.Strict);
            var mockConsultant = new ConsultantDTO
            {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
            };
            var mockConsultant2 = new ConsultantDTO
            {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
            };
            //ConsultantImplementation implementation = new ConsultantImplementation();
            consultant.Setup(r => r.GetConsultant(mockConsultant.RegID)).Returns(mockConsultant);
            consultant.Setup(r => r.GetConsultant(mockConsultant.RegID)).Returns(mockConsultant2);
            consultant.Verify();
            //var result = implementation.GetConsultant(mockConsultant.RegID);
            //var output = 0;
            Assert.IsAssignableFrom<ConsultantDTO>(mockConsultant);
            Assert.IsAssignableFrom<ConsultantDTO>(mockConsultant2);
            Assert.That(mockConsultant, Is.InstanceOf<ConsultantDTO>());
            Assert.That(mockConsultant, Is.InstanceOf<ConsultantDTO>());
            //Assert.AreNotEqual(result, mockConsultant);
           
            //Assert.Equals(mockConsultant, mockConsultant2);
        }


        [Test]
        public void GetConsultant_Should_Accept_Accept_Only_String_OfNumbers()
        {
            var regNo = "078091";
            var regLetter = "acdef";
            ConsultantImplementation implementation = new ConsultantImplementation();
            var consultantDTO = new Mock<IConsultant>();
            consultantDTO.Setup(c => c.GetConsultant(regNo)).Returns(new ConsultantDTO());
            consultantDTO.Setup(c => c.GetConsultant(regLetter)).Throws(new System.Exception());
            //Assert.That();
        }
        //IQueryable<ConsultantDTO> AllConsultant
        [Test]
        public void AllConsultant_Should_Return_IQueryable_Consultant_Object()
        {
            //Arrange
            var consultantDTO = new List<ConsultantDTO> {
               new ConsultantDTO
               {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
               },
                new ConsultantDTO
               {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
               },
                 new ConsultantDTO
               {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
               }
            };
            IQueryable<ConsultantDTO> consultants = consultantDTO.AsQueryable();
            var consultant = new Mock<IConsultant>();
            //consultant.Setup(d => d.AllConsultant()).Returns(consultants);
            Assert.That(consultants, Is.InstanceOf<IQueryable<ConsultantDTO>>());
        }


        [Test]
        public void AddConsultant_Should_Accept_All_DataTypes_Correctly()
        {
            //Arrange
            var consultant = new Consultant
            {
                FullName = "Salami",
                EmailAddress = "adetop99@gmail.com",
                MobileNo = "07034953469",
                DOB = "15-08-1990",
                RegID = "078091"
            };
            var consultants = new Mock<IConsultant>();
            consultants.Setup(d => d.AddConsultant(consultant)).Returns(Task.FromResult<Consultant>(consultant));
            Assert.That(consultant, Is.InstanceOf<Consultant>());
        }


    }
}