using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class AppointmentRequestController
    {
        List<AppointmentRequest> appointmentRequests;

        public AppointmentRequestController()
        {
            Load();
        }

        public List<AppointmentRequest> AppointmentRequests
        {
            get { return appointmentRequests; }
            set { appointmentRequests = value; }
        }

        void Load()
        {
            appointmentRequests = JsonSerializer.Deserialize<List<AppointmentRequest>>(File.ReadAllText("data/entities/AppointmentRequests.json"));
        }

        public AppointmentRequest FindById(int id)
        {
            foreach (AppointmentRequest appointmentRequest in appointmentRequests)
                if (appointmentRequest.Id == id)
                    return appointmentRequest;
            return null;
        }
    }
}
