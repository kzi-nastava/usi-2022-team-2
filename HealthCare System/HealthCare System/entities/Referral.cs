using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class Referral
    {
        int id;
        Specialization specialization;
        Doctor doctor;
        Patient patient;
        bool used;

        public Referral() { }

        public Referral(int id, Specialization specialization, Doctor doctor, Patient patient, bool used)
        {
            this.id = id;
            this.specialization = specialization;
            this.doctor = doctor;
            this.patient = patient;
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
            patient = referral.patient;
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
        public Patient Patient { get => patient; set => patient = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (doctor is null) doctorInfo = "null";
            else doctorInfo = Doctor.Jmbg;

            string patientInfo;
            if (patient is null) patientInfo = "null";
            else patientInfo = Patient.Jmbg;

            return "Refferal[" + "id: " + this.id + ", used: " + this.used + ", specialization: " + this.specialization.ToString() + 
                ", doctor: " + doctorInfo +", patient: " + patientInfo + "]";
        }
    }
}
