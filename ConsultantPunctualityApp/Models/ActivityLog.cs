using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.Models
{
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }




    }
    public class Orders
    {
        public int OrderId { get; set; }

        public string ProdunctName { get; set; }

        public int Quantity { get; set; }


        public decimal Rate { get; set; }

        public string CustomerId { get; set; }
    }
}