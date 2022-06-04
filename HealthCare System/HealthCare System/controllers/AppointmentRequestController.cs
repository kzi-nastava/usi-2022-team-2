using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using System;
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
            path = "../../../data/entities/AppointmentRequests.json";
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

        public void Serialize(string linkPath= "../../../data/links/AppointmentRequestLinker.csv")
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
                csv += request.Id.ToString() + ";"  + request.Patient.Jmbg + ";" 
                    + oldAppointmentInfo + ";" + newAppointmentInfo + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        public void RunAntiTrollCheck(Patient patient)
        {
            DateTime now = DateTime.Now;
            int createRequests = 0;
            int editRequests = 0;

            foreach (AppointmentRequest request in appointmentRequests)
            {
                if (request.Patient == patient)
                {
                    if ((now - request.RequestCreated).TotalDays < 30)
                    {
                        if (request.Type == RequestType.CREATE)
                            createRequests++;
                        else
                            editRequests++;
                    }
                }
            }

            if (createRequests> 7 || editRequests > 4)
                patient.Blocked = true;
        }

        public int GenerateId()
        {
            return appointmentRequests[^1].Id + 1;
        }
        public void Add(AppointmentRequestDto requestDto)
        {
            AppointmentRequest request = new(requestDto);
            appointmentRequests.Add(request);
            Serialize();
        }
    }
}
