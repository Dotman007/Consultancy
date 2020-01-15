using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;

namespace ConsultantPunctualityApp.Dependency
{
    public class ConsultantTaskImplementation : IConsultantTask
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _consultantDB;
        public ConsultantTaskImplementation(ConsultantDB consultantDB)
        {
            _consultantDB = consultantDB;
        }
        public async Task AddTask(ConsultantTask consultantTask)
        {
            logger.Info("Inside the AddTask Method");
            _consultantDB.ConsultantTasks.Add(consultantTask);
            await _consultantDB.SaveChangesAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(consultantTask));
        }
    }
}