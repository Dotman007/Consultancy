using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Dependency
{
    public interface IAssignment
    {
        Task AssignTaskToConsultant(Assignment assignment, string consultantId, int consultantTaskId, int assignerId);
        Task<List<GetAllAssignmentDTO>> GetAssignments();
        Task<AssignmentDTO> GetConsultantAssignmentsByPresentDate(string regNo);
        Task SubmitTask(string regNo, int consultantTaskId);
        //Task SubmitTask(string regNo, List<ConsultantTask> consultantTasks);
        Task<CompletedAssignmentDTO> GetAllAcheivedTask();
        Task<List<UnCompletedAssignmentDTO>> GetAllUnAchievedTask();

        Task<AssignmentDateDTO> GetConsultantAssignmentBySpecifiedDate(string regNo, string Date); 
    }
}
