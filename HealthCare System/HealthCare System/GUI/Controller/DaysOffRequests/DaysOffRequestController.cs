using HealthCare_System.Core.DaysOffRequests;
using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.DaysOffRequests
{
    public class DaysOffRequestController
    {
        IDaysOffRequestService daysOffRequestService;

        public DaysOffRequestController(IDaysOffRequestService daysOffRequestService)
        {
            this.daysOffRequestService = daysOffRequestService;
        }

        public List<DaysOffRequest> DaysOffRequests()
        {
            return daysOffRequestService.DaysOffRequests();
        }

        public DaysOffRequest FindById(int id)
        {
            return daysOffRequestService.FindById(id);
        }

        public int GenerateId()
        {
            return daysOffRequestService.GenerateId();
        }

        public void Serialize()
        {
            daysOffRequestService.Serialize();
        }

        public List<DaysOffRequest> FillterByDoctor(Doctor doctor)
        {
            return daysOffRequestService.FillterByDoctor(doctor);
        }

        public void Request(DaysOffRequestDto daysOffRequestDto)
        {
            daysOffRequestService.Request(daysOffRequestDto);
        }

        public void UrgentRequest(DaysOffRequestDto daysOffRequestDto)
        {
            daysOffRequestService.UrgentRequest(daysOffRequestDto);
        }

        public void AcceptDaysOffRequest(DaysOffRequest daysOffRequest)
        {
            daysOffRequestService.AcceptDaysOffRequest(daysOffRequest);
        }

        public void RejectDaysOffRequest(DaysOffRequest daysOffRequest, string message)
        {
            daysOffRequestService.RejectDaysOffRequest(daysOffRequest, message);
        }
    }
}
