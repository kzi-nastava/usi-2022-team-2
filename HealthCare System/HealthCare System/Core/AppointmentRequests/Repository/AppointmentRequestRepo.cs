using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.Core.AppotinmentRequests.Repository
{
    public class AppointmentRequestRepo
    {
        List<AppointmentRequest> appointmentRequests;
        string path;

        public AppointmentRequestRepo()
        {
            path = "../../../data/entities/AppointmentRequests.json";
            Load();
        }

        public AppointmentRequestRepo(string path)
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

        public void Serialize(string linkPath = "../../../data/links/AppointmentRequestLinker.csv")
        {
            string appointmentRequestsJson = JsonSerializer.Serialize(appointmentRequests,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, appointmentRequestsJson);
            string csv = "";
            foreach (AppointmentRequest request in appointmentRequests)
            {
                int newAppointmentInfo;
                int oldAppointmentInfo;
                if (request.NewAppointment is null) newAppointmentInfo = -1;
                else newAppointmentInfo = request.NewAppointment.Id;

                if (request.OldAppointment is null) oldAppointmentInfo = -1;
                else oldAppointmentInfo = request.OldAppointment.Id;
                csv += request.Id.ToString() + ";" + request.Patient.Jmbg + ";"
                    + oldAppointmentInfo + ";" + newAppointmentInfo + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        

        public int GenerateId()
        {
            return appointmentRequests[^1].Id + 1;
        }
        public void Add(AppointmentRequest request)
        {
            appointmentRequests.Add(request);
            Serialize();
        }
    }
}
