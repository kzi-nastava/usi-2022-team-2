using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.MedicalRecords.Model
{
    public class MedicalRecord
    {
        int id;
        Patient patient;
        double height;
        double weight;
        string diseaseHistory;
        List<Appointment> appointments;
        List<Ingredient> allergens;
        List<Prescription> prescriptions;
        List<Referral> referrals;

        public MedicalRecord() 
        {
            appointments = new List<Appointment>();
            allergens = new List<Ingredient>();
            prescriptions = new List<Prescription>();
            referrals = new List<Referral>();
        }

        public MedicalRecord(int id, Patient patient, double height, double weight, 
            string diseaseHistory, List<Appointment> appointments, List<Ingredient> allergens, 
            List<Prescription> prescriptions)
        {
            this.id = id;
            this.patient = patient;
            this.height = height;
            this.weight = weight;
            this.diseaseHistory = diseaseHistory;
            this.appointments = appointments;
            this.allergens = allergens;
            this.prescriptions = prescriptions;
            referrals = new List<Referral>();
        }

        public MedicalRecord(MedicalRecord medicalRecord)
        {
            id = medicalRecord.id;
            patient = medicalRecord.patient;
            height = medicalRecord.height;
            weight = medicalRecord.weight;
            diseaseHistory = medicalRecord.diseaseHistory;
            appointments = medicalRecord.appointments;
            allergens = medicalRecord.allergens;
            prescriptions = medicalRecord.prescriptions;
            referrals = medicalRecord.referrals;
        }

        public MedicalRecord(int id, double height, double weight, string diseaseHistory)
        {
            this.id = id;
            this.height = height;
            this.weight = weight;
            this.diseaseHistory = diseaseHistory;
            appointments = new List<Appointment>();
            allergens = new List<Ingredient>();
            prescriptions = new List<Prescription>();
            referrals = new List<Referral>();
        }

        public MedicalRecord(int id, double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            this.id = id;
            this.height = height;
            this.weight = weight;
            this.diseaseHistory = diseaseHistory;
            this.allergens = allergens;
            appointments = new List<Appointment>();
            prescriptions = new List<Prescription>();
            referrals = new List<Referral>();
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("height")]
        public double Height { get => height; set => height = value; }

        [JsonPropertyName("weight")]
        public double Weight { get => weight; set => weight = value; }

        [JsonPropertyName("diseaseHistory")]
        public string DiseaseHistory { get => diseaseHistory; set => diseaseHistory = value; }

        [JsonIgnore]
        public Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        public List<Appointment> Appointments { get => appointments; set => appointments = value; }

        [JsonIgnore]
        public List<Ingredient> Allergens { get => allergens; set => allergens = value; }

        [JsonIgnore]
        public List<Prescription> Prescriptions { get => prescriptions; set => prescriptions = value; }

        [JsonIgnore]
        public List<Referral> Referrals { get => referrals; set => referrals = value; }

        public bool IsDateValid(DateTime existingStart, DateTime existingEnd, DateTime newStart, DateTime newEnd)
        {
            return (existingStart <= newStart && existingEnd >= newStart) ||
                    (existingStart <= newEnd && existingEnd >= newEnd) ||
                    (newStart <= existingStart && newEnd >= existingEnd);
        }

        public void ValidatePrescription(Prescription prescription)
        {
            foreach (Prescription existing in prescriptions)
            {
                bool dateNotValid = IsDateValid(existing.Start, existing.End, prescription.Start, prescription.End);
                if (existing.Drug.Id == prescription.Drug.Id && dateNotValid)
                    throw new Exception("Chosen drug is already precribed in that time period!");
            }
        }

        public override string ToString()
        {
            string patientInfo;
            if (patient is null) patientInfo = "-1";
            else patientInfo = Patient.Jmbg;

            return "MedicalRecord[" + "id: " + id + ", patient: " + patientInfo +
                ", height: " + height + ", weight: " + weight + ", diseaseHistory: " + diseaseHistory + "]";
        }
    }
}
