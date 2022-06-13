using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Users.Model;

namespace HealthCare_System.Core.Referrals.Model
{
    public class ReferralDto
    {
        int id;
        Specialization specialization;
        Doctor doctor;
        MedicalRecord medicalRecord;
        bool used;

        public ReferralDto(int id, Specialization specialization, Doctor doctor, MedicalRecord medicalRecord, bool used)
        {
            this.id = id;
            this.specialization = specialization;
            this.doctor = doctor;
            this.medicalRecord = medicalRecord;
            this.used = used;
        }

        public int Id { get => id; set => id = value; }

        public Specialization Specialization { get => specialization; set => specialization = value; }

        public Doctor Doctor { get => doctor; set => doctor = value; }

        public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        public bool Used { get => used; set => used = value; }
    }
}
