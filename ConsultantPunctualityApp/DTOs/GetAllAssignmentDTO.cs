using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DTOs
{
    public class GetAllAssignmentDTO
    {
        public string AssignmentName { get; set; }

        public string AssignerName { get; set; }

        public string DateAssigned { get; set; }
    }
}