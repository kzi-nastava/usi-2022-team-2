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
            this.appointments = JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText("data/entities/Appointments.json"));
        }

        public Appointment FindById(int id)
        {
            foreach (Appointment appointment in this.appointments)
                if (appointment.Id == id)
                    return appointment;
            return null;
        }
        
        public int GenerateId()
        {
            return this.appointments[this.appointments.Count - 1].Id + 1;
        }

        public Appointment BookAppointment(DateTime start, DateTime end, AppointmentType type, Doctor doctor, Patient patient)
        {
            foreach (Appointment appointment in this.appointments)
            {
                if (doctor.Jmbg == appointment.Doctor.Jmbg || patient.Jmbg == appointment.Patient.Jmbg)
                    if ((start > appointment.Start && start < appointment.End) || (end > appointment.Start && end < appointment.End))
                        return null;
            }
            Appointment newAppointment = new Appointment(this.GenerateId(), start, end, doctor, patient, type);
            this.appointments.Add(newAppointment);
            this.Serialize();
            this.SyncLinkerFiles(newAppointment);
            return newAppointment;
        }

        public void SyncLinkerFiles(Appointment appointment)
        {
            using (FileStream fs = new FileStream("data/links/DoctorAppointment.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(appointment.Doctor.Jmbg + ";" + appointment.Id);
                    Console.WriteLine("AAAAAAAAA");
                }
            }

            using (FileStream fs = new FileStream("data/links/PatientAppointment.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(appointment.Patient.Jmbg + ";" + appointment.Id);
                }
            }
        }

        public void Serialize()
        {
            string jsonString = JsonSerializer.Serialize(this.appointments, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText("data/entities/Appointments.json", jsonString);
        }
    }
}
