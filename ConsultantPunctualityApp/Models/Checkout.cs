﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.Models
{
    public class Checkout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ConsultantId { get; set; }
        public virtual Consultant Consultant { get; set; }

        public string ConsultantRegID { get; set; }

        public string CheckoutDate { get; set; }

        public string CheckoutTime { get; set; }
        public bool CheckedOut { get; set; }
    }
}