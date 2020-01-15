using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultantPunctualityApp.DAL
{
    public class ConsultantDB : DbContext
    {
        public ConsultantDB():base("ConsultantConnection")
        {
        }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Assigner> Assigners { get; set; }
        public DbSet<CheckInOut> CheckInOuts { get; set; }
        //public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<ConsultantTask> ConsultantTasks { get; set; }
    }
}