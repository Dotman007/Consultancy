using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DTOs
{
    public class CheckinCheckoutDTO
    {
        //public int ConsultantId { get; set; }
        //public virtual Consultant Consultant { get; set; }

        public string ConsultantRegID { get; set; }


        public string ConsultantName { get; set; }

        public string CheckinDate { get; set; }

        public string CheckinTime { get; set; }
        //public bool ChekedIn { get; set; }

        public string CheckoutDate { get; set; }

        public string CheckoutTime { get; set; }
        //public bool CheckedOut { get; set; }
    }
}