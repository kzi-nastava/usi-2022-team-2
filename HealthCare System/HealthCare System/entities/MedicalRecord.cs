using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class MedicalRecord
    {
        int id;
        Patient patient;
        double height;
        double weight;
        string diseaseHistory;
        List<Appointment> appointments;
        List<Ingredient> allergens;

        public MedicalRecord() 
        {
            appointments = new List<Appointment>();
            allergens = new List<Ingredient>();
        }

        public MedicalRecord(int id, Patient patient, double height, double weight, string diseaseHistory, 
            List<Appointment> appointments, List<Ingredient> allergens)
        {
            this.id = id;
            this.patient = patient;
            this.height = height;
            this.weight = weight;
            this.diseaseHistory = diseaseHistory;
            this.appointments = appointments;
            this.allergens = allergens;
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
        }

        public MedicalRecord(int id, double height, double weight, string diseaseHistory)
        {
            this.id = id;
            this.height = height;
            this.weight = weight;
            this.diseaseHistory = diseaseHistory;
            appointments = new List<Appointment>();
            allergens = new List<Ingredient>();
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
        internal Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        internal List<Appointment> Appointments { get => appointments; set => appointments = value; }

        [JsonIgnore]
        internal List<Ingredient> Allergens { get => allergens; set => allergens = value; }

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
