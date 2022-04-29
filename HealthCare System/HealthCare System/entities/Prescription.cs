using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum DrugConsumption{BEFORE_MEAL, DURING_MEAL, AFTER_MEAL, UNIMPORTANT}
    public class Prescription
    {
        int id;
        MedicalRecord medicalRecord;
        DateTime start;
        DateTime end;
        int frequency;
        Drug drug;

        public Prescription() { }

        public Prescription(int id, MedicalRecord medicalRecord, DateTime start, DateTime end, 
            int frequency, Drug drug)
        {
            this.id = id;
            this.medicalRecord = medicalRecord;
            this.start = start;
            this.end = end;
            this.frequency = frequency;
            this.drug = drug;
        }

        public Prescription(int id, DateTime start, DateTime end, int frequency)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.frequency = frequency;
        }

        public Prescription(Prescription prescription)
        {
            id = prescription.id;
            medicalRecord = prescription.medicalRecord;
            start = prescription.start;
            end = prescription.end;
            frequency = prescription.frequency;
            drug = prescription.drug;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonIgnore]
        public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        [JsonPropertyName("start")]
        public DateTime Start { get => start; set => start = value; }

        [JsonPropertyName("end")]
        public DateTime End { get => end; set => end = value; }

        [JsonPropertyName("frequency")]
        public int Frequency { get => frequency; set => frequency = value; }

        [JsonIgnore]
        public Drug Drug { get => drug; set => drug = value; }

        public override string ToString()
        {
            return "Prescription[" + "id: " + id + ", medical record: " 
                + medicalRecord.Id.ToString() + ", start: " + start.ToString("dd/MM/yyyy HH:mm") 
                + ", end: " + end.ToString("dd/MM/yyyy HH:mm") + ", frequency: " 
                + frequency.ToString() + ", drug: " + drug.Id.ToString() + "]";
        }
    }
}
