using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Dependency
{
    public interface IConsultant
    {
        Task AddConsultant(Consultant consultant);
        List<ConsultantDTO> AllConsultant();
        ConsultantDTO GetConsultant(string id);
        string GenerateRegID();
    }
}
