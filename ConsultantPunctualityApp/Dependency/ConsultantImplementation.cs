using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;

namespace ConsultantPunctualityApp.Dependency
{
    public class ConsultantImplementation : IConsultant
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ConsultantDB _consultantdb = new ConsultantDB();
       
        public async Task AddConsultant(Consultant consultant)
        {
            logger.Info("Inside the AddConsultant Method");
            bool validate = (consultant.FullName != null && consultant.EmailAddress != null && consultant.MobileNo != null && consultant.DOB != null);
            if (validate)
                consultant.RegID = GenerateRegID();
                _consultantdb.Consultants.Add(consultant);
               await _consultantdb.SaveChangesAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(consultant));
        }

        public List<ConsultantDTO> AllConsultant()
        {
            logger.Info("Inside the AllConsultant Method");
            var consultants = _consultantdb.Consultants.Select(x => new ConsultantDTO
            {
                FullName = x.FullName,
                RegID = x.RegID,
                MobileNo = x.MobileNo,
                EmailAddress = x.EmailAddress,
                DOB = x.DOB
            });
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(consultants));
            return consultants.ToList();
        }

        public string GenerateRegID()
        {
            logger.Info("Inside the GenerateRegID Method");
            Random generator = new Random();
            string registrationNo  = generator.Next(0, 1000000).ToString("D6");
            if (registrationNo.Distinct().Count() ==1)
            {
                registrationNo = GenerateRegID();
            }
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(registrationNo));
            return registrationNo;
        }

        public ConsultantDTO GetConsultant(string id)
        {
            logger.Info("Inside the GetConsultant Method");
            var regLetters = "abcdefghjklmnopqrstuvwxyz";
            if (id.Contains(regLetters))
            {
                throw new InvalidOperationException("Not  a proper registration number");
            }
            var consultant = _consultantdb.Consultants.Select(x => new ConsultantDTO
            {
                FullName = x.FullName,
                RegID = x.RegID,
                EmailAddress = x.EmailAddress,
                MobileNo = x.MobileNo,
                DOB = x.DOB
            }).FirstOrDefault(c => c.RegID == id);
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(consultant));
            return consultant;
        }
    }
}