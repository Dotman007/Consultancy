using ConsultancyAppConsoleDemo.Functionalities;
using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultancyAppConsoleNetFrameworkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            APICall call = new APICall();
            var consultant = new Consultant();
            consultant.FullName = "Tunde Salami";
            consultant.EmailAddress = "tunde99@gmail.com";
            consultant.DOB = "1994/07/07";
            consultant.RegID = "456382";
            consultant.MobileNo = "07051641891";
            call.PostConsultant(consultant);

        }
    }
}
