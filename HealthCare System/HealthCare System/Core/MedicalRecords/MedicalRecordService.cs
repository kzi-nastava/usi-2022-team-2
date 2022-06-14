using System.Collections.Generic;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.MedicalRecords.Repository;

namespace HealthCare_System.Core.MedicalRecords
{
    public class MedicalRecordService : IMedicalRecordService
    {
        IMedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService(IMedicalRecordRepo medicalRecordRepo)
        {
            this.medicalRecordRepo = medicalRecordRepo;
        }

        public IMedicalRecordRepo MedicalRecordRepo { get => medicalRecordRepo; }

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
