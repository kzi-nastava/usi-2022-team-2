using HealthCare_System.Core.AppointmentRequests.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.AppotinmentRequests.Repository
{
    public interface IAppointmentRequestRepo
    {
        string Path { get; set; }

        List<AppointmentRequest> AppointmentRequests { get; set; }
        void Add(AppointmentRequest request);
        AppointmentRequest FindById(int id);
        int GenerateId();
        void Serialize(string linkPath = "../../../data/links/AppointmentRequestLinker.csv");
    }
}