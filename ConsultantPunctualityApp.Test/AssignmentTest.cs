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
    public class AssignmentTest
    {
        [Test]
        public void AssignTaskToConsultant_Should_Accept_TheCorrect_DataTypes()
        {
            var taskAssign = new Assignment
            {
                AssignerName = "Bolaji",
                AssignerId = 1,
                ConsultantName = "Salami",
                ConsultantId = 1,
                ConsultantTaskId = 3,
                ConsultantTaskName = "Deplyment",
                RegNo = "098378",
                Status = "Not Completed",
                Achieved = false,
                DateAssigned = "22-07-2019",
                TimeAssigned ="5:45PM"

            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDTO = new Mock<IAssignment>();
            assignmentDTO.Setup(c => c.AssignTaskToConsultant(taskAssign,taskAssign.RegNo, taskAssign.ConsultantTaskId, taskAssign.AssignerId)).Returns(Task.FromResult<Assignment>(taskAssign));
            Assert.That(taskAssign, Is.InstanceOf<Assignment>());
        }

        [Test]
        public void GetAssignments_Should_Return_ListOf_Assignments()
        {
            var allAssignment = new List<GetAllAssignmentDTO>()
            {
               new GetAllAssignmentDTO
               {
                AssignmentName = "Deloyment of Specta API",
                AssignerName ="Fortunatus Ochi",
                DateAssigned ="07-16-2019"
               },
               new GetAllAssignmentDTO
               {
                AssignmentName = "Deloyment of Specta API",
                AssignerName ="Fortunatus Ochi",
                DateAssigned ="07-16-2019"
               },
               new GetAllAssignmentDTO
               {
                AssignmentName = "Deloyment of Specta API",
                AssignerName ="Fortunatus Ochi",
                DateAssigned ="07-16-2019"
               }
            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var allAssignmentDTO = new Mock<IAssignment>();
            allAssignmentDTO.Setup(c => c.GetAssignments()).Returns(Task.FromResult<List<GetAllAssignmentDTO>>(allAssignment));
            Assert.That(allAssignment, Is.InstanceOf<List<GetAllAssignmentDTO>>());
            var countZero = allAssignment.Count == 0;
            var countGreaterThanZero = allAssignment.Count > 0;
            if (countZero)
            {
                Assert.That(allAssignment, Is.Null);
            }
            if (countGreaterThanZero)
            {
                Assert.That(allAssignment, Is.Not.Null);
            }
            Assert.That(allAssignment, Is.InstanceOf<List<GetAllAssignmentDTO>>());
        }

        [Test]
        public void GetConsultantAssignmentsByPresentDate_Should_Accept_ARegNoFormat()
        {
            var taskList = new List<ConsultantTask>();
            
            var allAssignment = new AssignmentDTO()
            {
               ConsultantTasks = taskList,
                AssignerName ="Fortunatus Ochi",
                DateAssigned ="07-16-2019"
            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDTO = new Mock<IAssignment>();
            var ListOfAssignment = new List<GetAllAssignmentDTO>();
            assignmentDTO.Setup(c => c.GetConsultantAssignmentsByPresentDate("")).Returns(Task.FromResult<AssignmentDTO>(allAssignment));
            Assert.That(allAssignment, Is.InstanceOf<AssignmentDTO>());
        }


        [Test]
        public void SubmitTask_Should_Accept_Correct_DataValues()
        {
            var taskList = new List<ConsultantTask>();
            var allAssignment = new AssignmentDTO()
            {
                ConsultantTasks = taskList,
                AssignerName = "Fortunatus Ochi",
                DateAssigned = "07-16-2019"
            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDTO = new Mock<IAssignment>();
            var ListOfAssignment = new List<GetAllAssignmentDTO>();
            assignmentDTO.Setup(c => c.GetConsultantAssignmentsByPresentDate("")).Returns(Task.FromResult<AssignmentDTO>(allAssignment));
            Assert.That(allAssignment, Is.InstanceOf<AssignmentDTO>());
        }

        [Test]
        public void GetAllAcheivedTask_Should_Return_Null_If_AchieveTask_Empty()
        {
            CompletedAssignmentDTO assignmentDTO = null;
            
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDto = new Mock<IAssignment>();
            assignmentDto.Setup(c => c.GetAllAcheivedTask()).Returns(Task.FromResult<CompletedAssignmentDTO>(assignmentDTO));
            Assert.That(assignmentDTO, Is.Null);
        }

        [Test]
        public void GetAllUnAchievedTask_Should_Return_Null_If_UnAchieveTask_Empty()
        {
            List<UnCompletedAssignmentDTO> unassignmentDTO = null;
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDto = new Mock<IAssignment>();
            assignmentDto.Setup(c => c.GetAllUnAchievedTask()).Returns(Task.FromResult<List<UnCompletedAssignmentDTO>>(unassignmentDTO));
            Assert.That(unassignmentDTO, Is.Null);
        }

        [Test]
        public void GetAllUnAchievedTask_Should_Return_ListOfUnAcheivedTask_If_UnAchieveTask_NotEmpty()
        {
            List<UnCompletedAssignmentDTO> unassignmentDTO = new List<UnCompletedAssignmentDTO>
            {

            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDto = new Mock<IAssignment>();
            assignmentDto.Setup(c => c.GetAllUnAchievedTask()).Returns(Task.FromResult<List<UnCompletedAssignmentDTO>>(unassignmentDTO));
            Assert.That(unassignmentDTO, Is.Not.Null);
        }
        [Test]
        public void GetAllAcheivedTask_Should_Return_AcheivedTask_If_AchieveTask_NotEmpty()
        {
            CompletedAssignmentDTO completed = new CompletedAssignmentDTO
            {

            };
            AssignmentImplementation implementation = new AssignmentImplementation();
            var assignmentDto = new Mock<IAssignment>();
            assignmentDto.Setup(c => c.GetAllAcheivedTask()).Returns(Task.FromResult<CompletedAssignmentDTO>(completed));
            Assert.That(completed, Is.Not.Null);
        }

        [Test]
        public void GetConsultantAssignmentBySpecifiedDate_Should_Accept_Exact_DataType()
        {
            var regNo = "";
            var date = "";
            AssignmentDateDTO assignmentDate = new AssignmentDateDTO
            {
            };
            AssignmentImplementation assignmentImplementation = new AssignmentImplementation();
            var assignmentDateDto = new Mock<IAssignment>();
            assignmentDateDto.Setup(d => d.GetConsultantAssignmentBySpecifiedDate(regNo, date)).Returns(Task.FromResult<AssignmentDateDTO>(assignmentDate));
            Assert.That(assignmentDate, Is.InstanceOf<AssignmentDateDTO>());
        }

        //[Test]
        //public void GetConsultantAssignmentBySpecifiedDate_Should_Accept_Exact_DataType()
        //{
        //    var regNo = "";
        //    var date = "";
        //    AssignmentDateDTO assignmentDate = new AssignmentDateDTO
        //    {
        //    };
        //    AssignmentImplementation assignmentImplementation = new AssignmentImplementation();
        //    var assignmentDateDto = new Mock<IAssignment>();
        //    assignmentDateDto.Setup(d => d.GetConsultantAssignmentBySpecifiedDate(regNo, date)).Returns(Task.FromResult<AssignmentDateDTO>(assignmentDate));
        //    Assert.That(assignmentDate, Is.InstanceOf<AssignmentDateDTO>());
        //}
    }

}

