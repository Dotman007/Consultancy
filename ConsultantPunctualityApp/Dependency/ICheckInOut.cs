using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Dependency
{
    public interface ICheckInOut
    {
        Task CheckIn(string regId);
        Task CheckOut(string regId);
        Task <List<CheckinCheckoutDTO>>GetCheckedInConsultants();
        Task<List<CheckinCheckoutDTO>> GetCheckedOutConsultants();
        Task <CheckinCheckoutDTO> GetCheckedInConsultant(string regId);
        Task <CheckinCheckoutDTO> GetCheckedOutConsultant(string regId);
    }
}
