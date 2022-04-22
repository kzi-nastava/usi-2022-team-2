using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class AppointmentController
    {
        List<Appointment> appointments;
        string path;

        public AppointmentController()
        {
            path = "data/entities/Appointments.json";
            Load();
        }

        public AppointmentController(string path)
        {
            this.path = path;
            Load();
        }

        void Load()
        {
            appointments = JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText(path));
        }

        internal List<Appointment> Appointments { get => appointments; set => appointments = value; }

        public string Path { get => path; set => path = value; }

        public Appointment FindById(int id)
        {
            foreach (Appointment appointment in appointments)
                if (appointment.Id == id)
                    return appointment;
            return null;
        }
        
        public int GenerateId()
        {
            return appointments[^1].Id + 1;
        }

        public void Serialize()
        {
            string appointmentsJson = JsonSerializer.Serialize(appointments, 
                new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(path, appointmentsJson);
        }
    }
}
