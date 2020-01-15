using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityAppMVC.Models
{
    public class ConsultantMVC
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string RegID { get; set; }

        public string MobileNo { get; set; }

        public string EmailAddress { get; set; }

        public string DOB { get; set; }

        public List<ConsultantTask> Tasks { get; set; }

    }
}