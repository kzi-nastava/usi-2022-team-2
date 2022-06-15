using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DaysOffRequests.Repository
{
    public interface IDaysOffRequestRepo
    {
        string Path { get; set; }

        List<DaysOffRequest> DaysOffRequests { get; set; }

        int GenerateId();

        DaysOffRequest FindById(int id);

        void Serialize(string linkPath = "../../../data/links/DaysOffRequest_Doctor.csv");

        List<DaysOffRequest> FillterByDoctor(Doctor doctor);

        void Add(DaysOffRequest daysOffRequest);
    }
}