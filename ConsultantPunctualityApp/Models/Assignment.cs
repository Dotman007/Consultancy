using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ConsultantTaskId { get; set; }
        public virtual ConsultantTask ConsultantTask { get; set; }

        public string ConsultantTaskName { get; set; }



        public int ConsultantId { get; set; }
        public virtual Consultant Consultant { get; set; }

        public string ConsultantName { get; set; }

        public int AssignerId { get; set; }
        public virtual Assigner Assigner { get; set; }


        public string AssignerName { get; set; }

        public string DateAssigned { get; set; }


        public string TimeAssigned { get; set; }


        public string DateAchieved { get; set; }


        public string TimeAchieved { get; set; }

        public string RegNo { get; set; }

        public string Status { get; set; }

        public bool Achieved { get; set; }

    }
}