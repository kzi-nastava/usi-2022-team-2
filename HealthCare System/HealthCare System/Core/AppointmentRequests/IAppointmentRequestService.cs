using HealthCare_System.Core.AppointmentRequests.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.AppotinmentRequests
{
    public interface IAppointmentRequestService
    {
        void AcceptRequest(AppointmentRequest request);
        void Add(AppointmentRequestDto requestDto);
        List<AppointmentRequest> AppointmentRequests();
        void RejectRequest(AppointmentRequest request);
    }
}