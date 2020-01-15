using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DTOs
{
    public class AssignmentDateDTO
    {
        public string RegNo { get; set; }

        public  string AssignerName { get; set; }

        public List<string> TaskName { get; set; }

        public string Status { get; set; }
    }
}