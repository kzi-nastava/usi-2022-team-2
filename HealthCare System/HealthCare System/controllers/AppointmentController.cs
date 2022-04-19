using HealthCare_System.entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class AppointmentController
    {
        List<Appointment> appointments;

        public AppointmentController()
        {
            this.LoadAppointments();
        }

        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

        void LoadAppointments()
        {
            this.appointments = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText("data/entities/Appointments.json"));
        }

        public Appointment FindById(int id)
        {
            foreach (Appointment appointment in this.appointments)
                if (appointment.Id == id)
                    return appointment;
            return null;
        }
    }
}
