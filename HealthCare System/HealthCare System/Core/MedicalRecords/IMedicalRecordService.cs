using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.MedicalRecords.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.MedicalRecords
{
    public interface IMedicalRecordService
    {
        MedicalRecordRepo MedicalRecordRepo { get; }

        MedicalRecord Add(double height, double weight, string diseaseHistory, List<Ingredient> allergens);
        void Delete(MedicalRecord medicalRecord);
        MedicalRecord FindById(int id);
        List<MedicalRecord> MedicalRecords();
        void Update(int id, double height, double weight, string diseaseHistory);
    }
}