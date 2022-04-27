using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class Patient : Person
    {
        MedicalRecord medicalRecord;
        bool blocked;

        public Patient() { }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password, MedicalRecord medicalRecord, bool blocked) : 
                base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.blocked = blocked;
        }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password, bool blocked) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.blocked = blocked;
        }

        public Patient(Patient patient) : base(patient.Jmbg, patient.FirstName, patient.LastName, patient.BirthDate, 
            patient.Mail, patient.Password)
        {
            medicalRecord = patient.medicalRecord;
            blocked = patient.blocked;
        }

        [JsonIgnore]
        public MedicalRecord MedicalRecord
        {
            get { return medicalRecord; }
            set { medicalRecord = value; }
        }

        [JsonPropertyName("blocked")]
        public bool Blocked
        {
            get { return blocked; }
            set { blocked = value; }
        }

        public List<Appointment> UpcomingAppointment(int nextDays)
        {
            return null;
        }

        public bool IsAvailable(DateTime start, DateTime end)
        {
            foreach (Appointment appointment in medicalRecord.Appointments)
            {
                if ((appointment.Start < start && appointment.End > start) ||
                    (appointment.Start < end && appointment.End > end))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
