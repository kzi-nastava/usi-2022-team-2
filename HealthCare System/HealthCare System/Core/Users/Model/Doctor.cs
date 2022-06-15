using HealthCare_System.Core.Appointments.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Users.Model
{
    public enum Specialization { GENERAL, SURGEON,  GYNECOLOGIST,NULL }

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

        public Doctor(string jmbg, string name, string lastName, DateTime birthDate, string mail,
            string password, List<Appointment> appointments, Specialization specialization,
            List<DateTime> freeDates) : base(jmbg, name, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
            this.specialization = specialization;
            this.freeDates = freeDates;
        }

        public Doctor(Doctor doctor) 
            : base(doctor.Jmbg, doctor.FirstName, doctor.LastName, doctor.BirthDate, doctor.Mail, doctor.Password)
        {
            appointments = doctor.appointments;
            specialization = doctor.specialization;
            freeDates = doctor.freeDates;
        }

        public Doctor(string jmbg, string name, string lastName, DateTime birthDate, string mail,
            string password, Specialization specialization, List<DateTime> freeDates) 
            : base(jmbg, name, lastName, birthDate, mail, password)
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

        public DateTime getNextFreeAppointment(DateTime start, DateTime end)
        {
            foreach (DateTime date in freeDates)
            {
                if (start.Date == date.Date)
                {
                    return start.AddDays(1);
                }
            }
            foreach (Appointment appointment in appointments)
            {
                if ((appointment.Start <= start && appointment.End >= start) ||
                    (appointment.Start <= end && appointment.End >= end) ||
                    (start <= appointment.Start && end >= appointment.End))
                {
                    return appointment.End.AddMinutes(1);
                }
            }
            return start;
        }

        public Appointment getNextAppointment(DateTime currentTime, int duration)
        {
            Appointment retAppointment = null;
            
            foreach (Appointment appointment in appointments)
            {
                if((appointment.Start > currentTime) && (retAppointment == null)) 
                { 
                    retAppointment = appointment;
                    continue;
                }
                if (appointment.Start > currentTime && 
                    appointment.Start < retAppointment.Start 
                    && appointment.Start.AddMinutes(duration) <= appointment.End)
                {
                    retAppointment = appointment;
                }
            }
            return retAppointment;

        }
    

        public DateTime getClosestFreeAppointment(int duration, Patient patient)
        {
            DateTime currentClosest = DateTime.Now;
            DateTime lastClosest = currentClosest;
            
            while (true)
            {
                currentClosest = getNextFreeAppointment(currentClosest, currentClosest.AddMinutes(duration));
                while (!(IsAvailable(currentClosest, currentClosest.AddMinutes(duration)) && patient.IsAvailable(currentClosest, currentClosest.AddMinutes(duration))))
                {
                    if (IsAvailable(currentClosest, currentClosest.AddMinutes(duration)) && !patient.IsAvailable(currentClosest, currentClosest.AddMinutes(duration)))
                    {
                        currentClosest = currentClosest.AddMinutes(duration);
                        continue;
                    }
                    currentClosest = getNextFreeAppointment(currentClosest, currentClosest.AddMinutes(duration));
                }
                if (currentClosest == lastClosest)
                {
                    break;
                }
                lastClosest = currentClosest;
            }

            return currentClosest;
        }

        public List<Appointment> FilterAppointments(DateTime date)
        {
            List<Appointment> upcoming = new();

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
