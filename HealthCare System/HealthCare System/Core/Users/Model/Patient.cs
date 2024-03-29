﻿using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.MedicalRecords.Model;
using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Users.Model
{
    public class Patient : Person
    {
        MedicalRecord medicalRecord;
        bool blocked;
        int minutesBeforeDrug;

        public Patient() { }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password, MedicalRecord medicalRecord, bool blocked, int minutesBeforeDrug) : 
                base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.blocked = blocked;
            this.minutesBeforeDrug = minutesBeforeDrug;
        }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password, bool blocked, int minutesBeforeDrug) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.blocked = blocked;
            this.minutesBeforeDrug = minutesBeforeDrug;
        }

        public Patient(Patient patient) : base(patient.Jmbg, patient.FirstName, patient.LastName,
            patient.BirthDate, patient.Mail, patient.Password)
        {
            medicalRecord = patient.medicalRecord;
            blocked = patient.blocked;
            minutesBeforeDrug = patient.minutesBeforeDrug;
        }

        [JsonIgnore]
        public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        [JsonPropertyName("blocked")]
        public bool Blocked { get => blocked; set => blocked = value; }
        
        [JsonPropertyName("minutesBeforeDrug")]
        public int MinutesBeforeDrug { get => minutesBeforeDrug; set => minutesBeforeDrug = value; }


        public bool IsAvailable(DateTime start, DateTime end)
        {
            foreach (Appointment appointment in medicalRecord.Appointments)
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
    }
}
