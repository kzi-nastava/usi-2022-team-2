using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum Specialization { GENERAL, SURGEON,  GYNECOLOGIST }

    public class Doctor : Person
    {
        List<Appointment> appointments;
        Specialization specialization;
        List<DateTime> freeDates;

        public Doctor()
        {
            appointments = new List<Appointment>();
            freeDates = new List<DateTime>();
        }

        public Doctor(string jmbg, string name, string lastName, DateTime birthDate, string mail, string password, 
            List<Appointment> appointments, Specialization specialization, List<DateTime> freeDates) : 
            base(jmbg, name, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
            this.specialization = specialization;
            this.freeDates = freeDates;
        }

        public Doctor(Doctor doctor) :
            base(doctor.Jmbg, doctor.FirstName, doctor.LastName, doctor.BirthDate, doctor.Mail, doctor.Password)
        {
            appointments = doctor.appointments;
            specialization = doctor.specialization;
            freeDates = doctor.freeDates;
        }

        public Doctor(string jmbg, string name, string lastName, DateTime birthDate, string mail, string password,
            Specialization specialization, List<DateTime> freeDates) :
            base(jmbg, name, lastName, birthDate, mail, password)
        {
            appointments = new List<Appointment>();
            this.specialization = specialization;
            this.freeDates = freeDates;
        }

        [JsonPropertyName("freeDates")]
        public List<DateTime> FreeDates { get => freeDates; set => freeDates = value; }

        [JsonIgnore]
        public List<Appointment> Appointments { get => appointments; set => appointments = value; }

        [JsonPropertyName("specialization")]
        public Specialization Specialization { get => specialization; set => specialization = value; }

        public bool IsAvailable(DateTime start, DateTime end)
        {
            foreach (DateTime date in freeDates)
            {
                if (start.Date == date.Date)
                {
                    return false;
                }
            }
            foreach (Appointment appointment in appointments)
            {
                if ((appointment.Start <= start && appointment.End >= start) ||
                    (appointment.Start <= end && appointment.End >= end) ||
                    (start <= appointment.Start && end >= appointment.End))
                {
                    return false;
                }
            }
            return true;
        }

        public List<Appointment> FilterAppointments(DateTime date)
        {
            List<Appointment> upcoming = new List<Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Status == AppointmentStatus.FINISHED) continue;
                if (appointment.Start < date) continue;
                if (appointment.Start.Subtract(date).Days > 3) continue;

                upcoming.Add(appointment);
            }

            return upcoming;
        }

    }
}
