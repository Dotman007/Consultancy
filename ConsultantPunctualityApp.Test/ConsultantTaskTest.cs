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
    public class ConsultantTaskTest
    {
        [Test]
        public void AddTask_Should_Accept_Correct_Data()
        {
            var testData = new ConsultantPunctualityApp.Models.ConsultantTask()
            {

            };
            var mocktestData = new Mock<IConsultantTask>();
            mocktestData.Setup(m => m.AddTask(testData)).Returns(Task.FromResult<ConsultantTask>(testData));
            Assert.That(testData, Is.InstanceOf<ConsultantTask>());
        }
    }
}
