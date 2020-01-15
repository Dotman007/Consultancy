using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ConsultantPunctualityApp.Dependency
{
    public class AssignerImplementation : IAssigner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ConsultantDB _consultantdb = new ConsultantDB();
        public AssignerImplementation()
        {
            
        }

        public string GeneratePassword()
        {
            logger.Info("Inside the GeneratePassword Method");
            const int size = 10;
            Random rand = new Random((int)DateTime.Now.Ticks);
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, size).Select(x => input[rand.Next(0, input.Length)]).ToArray());
        }
        public async Task Register(Assigner assigner)
        {
            logger.Info("Inside the Register Method");
            bool validateForm = (assigner.Name != null && assigner.Position != null && assigner.Username != null);
            if (validateForm)
            {
                logger.Info("Inside the validateForm condition");
                assigner.Password = GeneratePassword();
                 _consultantdb.Assigners.Add(assigner);
                await _consultantdb.SaveChangesAsync();
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(assigner));
            }
        }
    }
}