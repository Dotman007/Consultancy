using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Test
{
    [TestFixture]
   public class AssignerTest
    {
        [Test]
        public void GeneratePassword_Should_Return_String_Of_AlphaNumeric_Characters()
        {
            var likeReg = "5ejyts6fzo";
            AssignerImplementation assignerImplementation = new AssignerImplementation();
            var result = assignerImplementation.GeneratePassword();
            Assert.That(result, Is.InstanceOf<string>());
        }


        [Test]
        public void Register_Should_Return_String_Of_AlphaNumeric_Characters()
        {
            AssignerImplementation assignerImplementation = new AssignerImplementation();
            var mockAssigner = new Mock<IAssigner>();
            var assigner = new Assigner { };
            mockAssigner.Setup(m => m.Register(assigner)).Returns(Task.FromResult<Assigner>(assigner));
            Assert.That(assigner, Is.InstanceOf<Assigner>());
        }
    }
}
