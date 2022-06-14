using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Appointments.Repository
{

    public class AppointmentRepo : IAppointmentRepo
    {
        List<Appointment> appointments;
        string path;

        public AppointmentRepo()
        {
            path = "../../../data/entities/Appointments.json";
            Load();
        }

        public AppointmentRepo(string path)
        {
            this.path = path;
            Load();
        }

        void Load()
        {
            appointments = JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText(path));
        }

        public List<Appointment> Appointments { get => appointments; set => appointments = value; }

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

        public void Serialize(string linkPath = "../../../data/links/AppointmentLinker.csv")
        {
            string appointmentsJson = JsonSerializer.Serialize(appointments,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, appointmentsJson);
            string csv = "";
            foreach (Appointment appointment in appointments)
            {
                int roomId;
                if (appointment.Room == null)
                {
                    roomId = -1;
                }
                else
                {
                    roomId = appointment.Room.Id;
                }
                csv += appointment.Id.ToString() + ";" + appointment.Doctor.Jmbg + ";" + appointment.Patient.Jmbg + ";" + roomId.ToString() + ";" + appointment.Anamnesis.Id.ToString() + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        //TODO: Move somewhere?
        public List<Appointment> FindByWord(Patient patient, string word)
        {
            List<Appointment> results = new();
            foreach (Appointment appointment in patient.MedicalRecord.Appointments)
            {
                if (appointment.Anamnesis.Description.ToLower().Contains(word.ToLower()))
                    results.Add(appointment);
            }
            return results;
        }

        public List<Appointment> FindPastAppointments(Patient patient)
        {
            List<Appointment> appointments = new();
            foreach (Appointment appointment in patient.MedicalRecord.Appointments)
            {
                if (DateTime.Now > appointment.Start && appointment.Status != AppointmentStatus.ON_HOLD)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public List<Appointment> FindUpcomingAppointments(Patient patient)
        {
            List<Appointment> appointments = new();
            foreach (Appointment appointment in patient.MedicalRecord.Appointments)
            {
                if (DateTime.Now < appointment.Start && appointment.Status != AppointmentStatus.ON_HOLD)
                {
                    appointments.Add(appointment);
                }
            }
            List<Appointment> sortedAppoinments = appointments.OrderBy(x => x.Start).ToList();
            return sortedAppoinments;
        }



    }
}
