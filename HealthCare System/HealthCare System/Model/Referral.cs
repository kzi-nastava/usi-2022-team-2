using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class Referral
    {
        int id;
        Specialization specialization;
        Doctor doctor;
        MedicalRecord medicalRecord;
        bool used;

        public Referral() { }

        public Referral(int id, Specialization specialization, Doctor doctor, MedicalRecord medicalRecord, bool used)
        {
            this.id = id;
            this.specialization = specialization;
            this.doctor = doctor;
            this.medicalRecord = medicalRecord;
            this.used = used;
        }

        public Referral(int id, Specialization specialization, bool used)
        {
            this.id = id;
            this.specialization = specialization;
            this.used = used;
        }

        public Referral(Referral referral)
        {
            id = referral.id;
            specialization = referral.specialization;
            doctor = referral.doctor;
            medicalRecord = referral.medicalRecord;
            used = referral.used;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("used")]
        public bool Used { get => used; set => used = value; }

        [JsonPropertyName("specialization")]
        public Specialization Specialization { get => specialization; set => specialization = value; }

        [JsonIgnore]
        public Doctor Doctor { get => doctor; set => doctor = value; }

        [JsonIgnore]
        public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (doctor is null) doctorInfo = "null";
            else doctorInfo = Doctor.Jmbg;

            string medicalRecordInfo;
            if (medicalRecord is null) medicalRecordInfo = "null";
            else medicalRecordInfo = medicalRecord.Id.ToString();

            return "Refferal[" + "id: " + id + ", used: " + used + ", specialization: " 
                + specialization.ToString() + ", doctor: " + doctorInfo +", medicalRecord: " + medicalRecordInfo + "]";
        }
    }
}
