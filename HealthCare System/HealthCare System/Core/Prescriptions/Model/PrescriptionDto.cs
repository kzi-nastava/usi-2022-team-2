using System;

namespace HealthCare_System.Core.Prescriptions.Model
{
    public class PrescriptionDto
    {
        int id;
        MedicalRecord medicalRecord;
        DateTime start;
        DateTime end;
        int frequency;
        Drug drug;

        public PrescriptionDto(int id, MedicalRecord medicalRecord, DateTime start, DateTime end, int frequency, Drug drug)
        {
            this.id = id;
            this.medicalRecord = medicalRecord;
            this.start = start;
            this.end = end;
            this.frequency = frequency;
            this.drug = drug;
        }

        public int Id { get => id; set => id = value; }

        public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        public DateTime Start { get => start; set => start = value; }

        public DateTime End { get => end; set => end = value; }

        public int Frequency { get => frequency; set => frequency = value; }

        public Drug Drug { get => drug; set => drug = value; }
    }
}
