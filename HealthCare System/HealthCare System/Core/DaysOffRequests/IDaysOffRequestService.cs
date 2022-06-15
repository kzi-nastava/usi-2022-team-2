using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DaysOffRequests
{
    public interface IDaysOffRequestService
    {
        List<DaysOffRequest> DaysOffRequests();

        public IDaysOffNotificationService DaysOffNotificationService { get ; set ; }

        DaysOffRequest FindById(int id);

        int GenerateId();

        void Serialize();

        List<DaysOffRequest> FillterByDoctor(Doctor doctor);

        void Request(DaysOffRequestDto daysOffRequestDto);

        void UrgentRequest(DaysOffRequestDto daysOffRequestDto);

        public void AcceptDaysOffRequest(DaysOffRequest daysOffRequest);

        public void RejectDaysOffRequest(DaysOffRequest daysOffRequest, string message);
    }
}