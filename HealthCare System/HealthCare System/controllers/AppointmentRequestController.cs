using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class AppointmentRequestController
    {
        List<AppointmentRequest> appointmentRequests;

        public AppointmentRequestController()
        {
            this.LoadAppointmentRequests();
        }

        public List<AppointmentRequest> AppointmentRequests
        {
            get { return appointmentRequests; }
            set { appointmentRequests = value; }
        }

        void LoadAppointmentRequests()
        {
            this.appointmentRequests = JsonSerializer.Deserialize<List<AppointmentRequest>>(File.ReadAllText("data/entities/AppointmentRequests.json"));
        }

        public AppointmentRequest FindById(int id)
        {
            foreach (AppointmentRequest appointmentRequest in this.appointmentRequests)
                if (appointmentRequest.Id == id)
                    return appointmentRequest;
            return null;
        }
    }
}
