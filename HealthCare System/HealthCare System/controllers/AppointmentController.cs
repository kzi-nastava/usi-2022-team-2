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

        public AppointmentController()
        {
            Load();
        }

        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

        void Load()
        {
            appointments = JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText("data/entities/Appointments.json"));
        }

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

        public Appointment BookAppointment(DateTime start, DateTime end, AppointmentType type, Doctor doctor, Patient patient)
        {
            foreach (Appointment appointment in appointments)
            {
                if (doctor.Jmbg == appointment.Doctor.Jmbg || patient.Jmbg == appointment.Patient.Jmbg)
                    if ((start > appointment.Start && start < appointment.End) || (end > appointment.Start && end < appointment.End))
                        return null;
            }
            Appointment newAppointment = new();
            appointments.Add(newAppointment);
            Serialize();
            SyncLinkerFiles(newAppointment);
            return newAppointment;
        }

        public void SyncLinkerFiles(Appointment appointment)
        {
            using (FileStream fs = new("data/links/DoctorAppointment.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new(fs))
                {
                    sw.WriteLine(appointment.Doctor.Jmbg + ";" + appointment.Id);
                    Console.WriteLine("AAAAAAAAA");
                }
            }

            using (FileStream fs = new("data/links/PatientAppointment.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new(fs))
                {
                    sw.WriteLine(appointment.Patient.Jmbg + ";" + appointment.Id);
                }
            }
        }

        public void Serialize()
        {
            string jsonString = JsonSerializer.Serialize(appointments, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText("data/entities/Appointments.json", jsonString);
        }
    }
}
