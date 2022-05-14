using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    public enum AnamnesesSortCriterium { DATE, DOCTOR, SPECIALIZATION};
    public enum SortDirection { ASCENDING, DESCENDING};
    class AppointmentController
    {
        List<Appointment> appointments;
        string path;

        public AppointmentController()
        {
            path = "../../../data/entities/Appointments.json";
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

        public Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration, Patient patient)
        {
            Dictionary<Appointment, DateTime> appointmentsDict = new Dictionary<Appointment, DateTime>();
            DateTime currentTime = DateTime.Now;
            Appointment currentAppointment;

            foreach (Appointment appointment in appointments)
            {
                if (doctors.Contains(appointment.Doctor) && appointment.Start > currentTime && appointment.Start <= currentTime.AddHours(2))
                {
                    appointmentsDict[appointment] = appointment.Doctor.getNextFreeAppointment(appointment.Start, appointment.End);
                    while (!((appointment.Doctor.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration)) &&
                        appointment.Patient.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration)))))
                    {
                        appointmentsDict[appointment] = appointment.Doctor.getNextFreeAppointment(appointmentsDict[appointment], 
                                                                                                  appointmentsDict[appointment].AddMinutes(duration));
                    }

                }
                if (appointmentsDict.Count == 5) { break; }
            }
            return appointmentsDict;
        }


        public int GenerateId()
        {
            return appointments[^1].Id + 1;
        }

        public void Serialize(string linkPath= "../../../data/links/AppointmentLinker.csv")
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
                csv += appointment.Id.ToString() + ";" + appointment.Doctor.Jmbg + ";" + appointment.Patient.Jmbg + ";" + roomId.ToString() + ";" + appointment.Anamnesis.Id.ToString()+"\n";
            }
            File.WriteAllText(linkPath, csv);
        }

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
        public List<Appointment> SortByDate(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction==SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Start).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Start).ToList();

        }
        public List<Appointment> SortByDoctor(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction == SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Doctor.FirstName).ThenBy(x => x.Doctor.LastName).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Doctor.FirstName).ThenByDescending(x => x.Doctor.LastName).ToList();

        }
        public List<Appointment> SortBySpecialization(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction == SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Doctor.Specialization).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Doctor.Specialization).ToList();

        }
        public List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium, SortDirection direction)
        {
            List<Appointment> unsortedAnamneses = FindByWord(patient, word);
            if (criterium == AnamnesesSortCriterium.DATE)
            {
                return SortByDate(unsortedAnamneses, direction);
            }
            else if (criterium == AnamnesesSortCriterium.DOCTOR)
            {
                return SortByDoctor(unsortedAnamneses, direction);
            }
            else
            {
                return SortBySpecialization(unsortedAnamneses, direction);
            }
        }
    }
}
