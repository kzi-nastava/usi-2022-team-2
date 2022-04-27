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

        public Prescription(int id, MedicalRecord medicalRecord, DateTime start, DateTime end, int frequency, Drug drug)
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
            this.id = prescription.id;
            this.medicalRecord = prescription.medicalRecord;
            this.start = prescription.start;
            this.end = prescription.end;
            this.frequency = prescription.frequency;
            this.drug = prescription.drug;
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
            return "Prescription[" + "id: " + this.id + ", medical record: " + this.medicalRecord.Id.ToString() + ", start: " + this.start.ToString("dd/MM/yyyy HH:mm") + ", end: " + this.end.ToString("dd/MM/yyyy HH:mm") + ", frequency: " + this.frequency.ToString() + ", drug: " + this.drug.Id.ToString() + "]";
        }
    }
}
