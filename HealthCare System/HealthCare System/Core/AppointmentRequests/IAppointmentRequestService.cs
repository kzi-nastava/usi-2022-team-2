using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.AppotinmentRequests.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.AppotinmentRequests
{
    public interface IAppointmentRequestService
    {
        public IAppointmentRequestRepo AppointmentRequestRepo { get; }
        void AcceptRequest(AppointmentRequest request);
        void Add(AppointmentRequestDto requestDto);
        List<AppointmentRequest> AppointmentRequests();
        void RejectRequest(AppointmentRequest request);
        public AppointmentRequest FindById(int id);

        public void Serialize(string linkPath = "../../../data/links/AppointmentRequestLinker.csv");
        public int GenerateId();
    }
}