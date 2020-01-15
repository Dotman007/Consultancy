using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;
namespace ConsultantPunctualityApp.Dependency
{
    public class AssignmentImplementation : IAssignment
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ConsultantDB _consultantDB = new ConsultantDB();
        //public AssignmentImplementation(ConsultantDB consultantDB)
        //{
        //    _consultantDB = consultantDB;
        //}

        public async Task AssignTaskToConsultant(Assignment assignment,  string consultantId, int consultantTaskId, int assignerId)
        {
            try
            {
                logger.Info("Inside the Assign Task To Consultant Method");
                var getConsultant = await _consultantDB.Consultants.Where(g => g.RegID == consultantId).SingleOrDefaultAsync();
                if (getConsultant == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent(string.Format("The Registration Number do not exist")),
                        ReasonPhrase = "The Registration Number do not exist"
                    };
                    logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                    throw new HttpResponseException(response);
                }
                var getTask = await _consultantDB.ConsultantTasks.Where(t => t.TaskId == consultantTaskId).SingleOrDefaultAsync();
                if (getTask == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent(string.Format("The Task does not exist kindly add the task")),
                        ReasonPhrase = "The Task does not exist kindly add the task"
                    };
                    logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                    throw new HttpResponseException(response);
                }
                var getAssigner = await _consultantDB.Assigners.Where(a => a.Id == assignerId).SingleOrDefaultAsync();
                if (true)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent(string.Format("Invalid Assigner Id")),
                        ReasonPhrase = "Invalid Assigner Id"
                    };
                    logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                }

                var taskAssign = new Assignment
                {
                    AssignerName = getAssigner.Name,
                    AssignerId = getAssigner.Id,
                    ConsultantName = getConsultant.FullName,
                    ConsultantId = getConsultant.Id,
                    ConsultantTaskId = getTask.TaskId,
                    ConsultantTaskName = getTask.TaskName,
                    RegNo = getConsultant.RegID,
                    Status = "Not Completed",
                    Achieved = false,
                    DateAssigned = DateTime.Now.ToString("MM-dd-yyyy"),
                    TimeAssigned = DateTime.Now.ToString("HH:mm tt")
                };

                _consultantDB.Assignments.Add(taskAssign);
                await _consultantDB.SaveChangesAsync();
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(taskAssign));
            }
            catch (DbEntityValidationException ex)
            {

                throw ex;
            }
            
        }

        public  async Task<List<GetAllAssignmentDTO>> GetAssignments()
        {
            logger.Info("Inside the GetAssignments Method");
            List<GetAllAssignmentDTO> assignments = await _consultantDB.Assignments.Select(s=> new GetAllAssignmentDTO {
                AssignmentName = s.ConsultantTaskName,
                AssignerName = s.AssignerName,
                DateAssigned = s.DateAssigned
            }).ToListAsync();
            if (assignments.Count ==0)
            {
                return null;
            }
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(assignments));
            return assignments;

        }

        public async Task<AssignmentDTO> GetConsultantAssignmentsByPresentDate(string regNo)
        {
            logger.Info("Inside the GetConsultantAssignmentsByPresentDate Method");
            var todaysDate = DateTime.Now.ToString("MM-dd-yyyy");
            var listOfAssignments = _consultantDB.ConsultantTasks.ToList();
            var assignments = _consultantDB.Assignments.Where(c => c.RegNo == regNo).ToList();
            List<string> taskList = new List<string>();
            foreach (var listOfAssignment in assignments)
            {
                taskList.Add(listOfAssignment.ConsultantTaskName);
            }
            var consultantTasks = _consultantDB.Assignments.Where(c => c.RegNo == regNo).ToList();
            var getConsultant = await _consultantDB.Assignments.Where(g => g.RegNo == regNo && g.DateAssigned == todaysDate).Select(c => new AssignmentDTO
            {
                ConsultantName = c.ConsultantName,
                RegNo = c.RegNo,
                AssignerName = c.AssignerName,
                DateAssigned = c.DateAssigned,
                DateAchieved = c.DateAchieved,
                TimeAssigned = c.TimeAssigned,
                TimeAchieved = c.TimeAchieved,
                Achieved = c.Achieved,
                Status= c.Status,
                TaskName =  taskList
            }).SingleOrDefaultAsync();
            //var getTaskByRegNo = await _consultantDB.Assignments.Where(t => t.RegNo == regNo).ToListAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(getConsultant));
            return getConsultant;
        }

        public async Task SubmitTask(string regNo, int consultantTaskId)
        {
            logger.Info("Inside the SubmitTask Method");
            var getConsultant = await _consultantDB.Assignments.Where(c => c.RegNo == regNo && c.ConsultantTaskId == consultantTaskId).SingleOrDefaultAsync();
            getConsultant.Achieved = true;
            getConsultant.Status = "Completed";
            getConsultant.DateAchieved = DateTime.Now.ToString("MM-dd-yyyy");
            getConsultant.TimeAchieved = DateTime.Now.ToString("MM:hh tt");
            await _consultantDB.SaveChangesAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(getConsultant));
        }

        public  Task<CompletedAssignmentDTO> GetAllAcheivedTask()
        {
            logger.Info("Inside the GetAllAcheivedTask Method");
            List<string> task = new List<string>();
            var completedTasks = _consultantDB.Assignments.Where(c => c.Achieved == true).Distinct();
            foreach (var completedTask in completedTasks)
            {
                task.Add(completedTask.ConsultantTaskName);
            }
            var getAchievedTask = _consultantDB.Assignments.Include("Consultant").Include("Assigner").Include("ConsultantTask").Where(c=>c.Achieved == true).Select(c => new CompletedAssignmentDTO
            {
                
                AssignerName = c.Assigner.Name,
                TimeAssigned = c.TimeAssigned,
                DateAssigned = c.DateAssigned,
                TimeAchieved = c.TimeAchieved,
                DateAchieved = c.DateAchieved,
                RegNo = c.RegNo,
                Status = c.Status,
                ConsultantName = c.ConsultantName,
                ConsultantTasks = task
            }).FirstOrDefaultAsync();
            if (getAchievedTask == null)
            {
                return null;
            }
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(getAchievedTask));
            return getAchievedTask;
        }

        public async Task<List<UnCompletedAssignmentDTO>> GetAllUnAchievedTask()
        {
            logger.Info("Inside the GetAllUnAchievedTask Method");
            var UngetAchievedTask = await _consultantDB.Assignments.Include("Consultant").Include("Assigner").Include("ConsultantTask").Where(c => c.Achieved != true).Select(c => new UnCompletedAssignmentDTO
            {
                ConsultantTaskName = c.ConsultantTaskName,
                AssignerName = c.Assigner.Name,
                TimeAssigned = c.TimeAssigned,
                DateAssigned = c.DateAssigned,
                RegNo = c.RegNo,
                Status = c.Status,
                ConsultantName = c.ConsultantName
            }).ToListAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(UngetAchievedTask));
            return UngetAchievedTask;
        }

        public async Task<AssignmentDateDTO> GetConsultantAssignmentBySpecifiedDate(string regNo, string Date)
        {
            logger.Info("Inside the GetConsultantAssignmentBySpecifiedDate Method");
            var listOfAssignments = _consultantDB.ConsultantTasks.Distinct();
            var assignments = _consultantDB.Assignments.Where(c => c.RegNo == regNo && c.DateAssigned == Date).Distinct();
            List<string> taskList = new List<string>();
            foreach (var listOfAssignment in assignments)
            {
                taskList.Add(listOfAssignment.ConsultantTaskName);
            }
            var getAssignmentByDate = await _consultantDB.Assignments.Where(d => d.RegNo == regNo && d.DateAssigned == Date).Select(c => new AssignmentDateDTO
            {
                RegNo = c.RegNo,
                AssignerName = c.AssignerName,
                TaskName = taskList,
                Status = c.Status
            }).FirstOrDefaultAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(getAssignmentByDate));
            return getAssignmentByDate;
        }
    }

}

