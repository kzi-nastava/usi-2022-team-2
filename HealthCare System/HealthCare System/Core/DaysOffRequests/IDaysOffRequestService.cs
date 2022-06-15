using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DaysOffRequests
{
    interface IDaysOffRequestService
    {
        List<DaysOffRequest> DaysOffRequests();

        DaysOffRequest FindById(int id);

        int GenerateId();

        void Serialize();

        List<DaysOffRequest> FillterByDoctor(Doctor doctor);

        void Request(DaysOffRequestDto daysOffRequestDto);

        void UrgentRequest(DaysOffRequestDto daysOffRequestDto);
    }
}