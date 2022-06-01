using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.MedicalRecordService
{
    class MedicalRecordService
    {
        public MedicalRecord Add(double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            MedicalRecord medRecord = new(medicalRecordRepo.GenerateId(), height, weight, diseaseHistory, allergens);
            medicalRecordRepo.Add(medRecord);
            return medRecord;
        }
        public void UpdateMedicalRecord(int id, double height, double weight, string diseaseHistory)
        {
            MedicalRecord medicalRecord = FindById(id);
            medicalRecord.Height = height;
            medicalRecord.Weight = weight;
            medicalRecord.DiseaseHistory = diseaseHistory;
            Serialize();
        }
    }
}
