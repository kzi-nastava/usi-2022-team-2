using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class AppointmentRequestController
    {
        List<AppointmentRequest> appointmentRequests;
        string path;

        public AppointmentRequestController()
        {
            path = "data/entities/AppointmentRequests.json";
            Load();
        }

        public AppointmentRequestController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<AppointmentRequest> AppointmentRequests { get => appointmentRequests; set => appointmentRequests = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            appointmentRequests = JsonSerializer.Deserialize<List<AppointmentRequest>>(File.ReadAllText(path));
        }

        public AppointmentRequest FindById(int id)
        {
            foreach (AppointmentRequest appointmentRequest in appointmentRequests)
                if (appointmentRequest.Id == id)
                    return appointmentRequest;
            return null;
        }

        public void Serialize()
        {
            string appointmentRequestsJson = JsonSerializer.Serialize(appointmentRequests, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, appointmentRequestsJson);
        }
    }
}
