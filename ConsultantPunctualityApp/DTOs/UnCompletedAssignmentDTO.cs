using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DTOs
{
    public class UnCompletedAssignmentDTO
    {
        public string ConsultantTaskName { get; set; }

        public string AssignerName { get; set; }

        public string DateAssigned { get; set; }

        public string TimeAssigned { get; set; }

        public string RegNo { get; set; }

        public string ConsultantName { get; set; }

        public string Status { get; set; }
                                           // public List<ConsultantTask> ConsultantTasks { get; set; }
    }
}