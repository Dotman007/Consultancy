using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DTOs
{
    public class AssignmentDTO
    {


        public string ConsultantName { get; set; }

        public string RegNo { get; set; }

        public List<Assigner> Assigner { get; set; }


        public string AssignerName { get; set; }

        public string DateAssigned { get; set; }


        public string TimeAssigned { get; set; }


        public string DateAchieved { get; set; }


        public string TimeAchieved { get; set; }

        
        public bool Achieved { get; set; }


        public string Status { get; set; }

        public List<ConsultantTask> ConsultantTasks { get; set; }

        public List<string> TaskName { get; set; }
    }
}