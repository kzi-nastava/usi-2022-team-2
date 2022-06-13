using System.Collections.Generic;
using HealthCare_System.Repository.MedicalRecordRepo;
using HealthCare_System.Model;

namespace HealthCare_System.Core.MedicalRecords
{
    public class MedicalRecordService
    {
        MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService(MedicalRecordRepo medicalRecordRepo)
        {
            this.medicalRecordRepo = medicalRecordRepo;
        }

        public MedicalRecordRepo MedicalRecordRepo { get => medicalRecordRepo; }

        public List<MedicalRecord> MedicalRecords()
        {
            return medicalRecordRepo.MedicalRecords;
        }

        public MedicalRecord FindById(int id)
        {
            return medicalRecordRepo.FindById(id);
        }

        public MedicalRecord Add(double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            MedicalRecord medRecord = new(medicalRecordRepo.GenerateId(), height, weight, diseaseHistory, allergens);
            medicalRecordRepo.Add(medRecord);
            return medRecord;
        }

        public void Update(int id, double height, double weight, string diseaseHistory)
        {
            MedicalRecord medicalRecord = medicalRecordRepo.FindById(id);
            medicalRecord.Height = height;
            medicalRecord.Weight = weight;
            medicalRecord.DiseaseHistory = diseaseHistory;
            medicalRecordRepo.Serialize();
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            medicalRecordRepo.Delete(medicalRecord);
        }
    }
}
