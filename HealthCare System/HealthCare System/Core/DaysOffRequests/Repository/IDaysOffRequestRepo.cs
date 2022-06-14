using HealthCare_System.Core.DaysOffRequests.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DaysOffRequests.Repository
{
    public interface IDaysOffRequestRepo
    {
        string Path { get; set; }

        List<DaysOffRequest> DaysOffRequests { get; set; }
        DaysOffRequest FindById(int id);
        void Serialize();
    }
}