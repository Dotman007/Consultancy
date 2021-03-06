﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.Models
{
    public class Consultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string RegID { get; set; }

        public string MobileNo { get; set; }

        public string EmailAddress { get; set; }

        public string DOB { get; set; }

        public List<ConsultantTask> Tasks { get; set; }


    }
}