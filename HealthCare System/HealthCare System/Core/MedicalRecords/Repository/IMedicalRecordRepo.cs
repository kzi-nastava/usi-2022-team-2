using HealthCare_System.Core.MedicalRecords.Model;

namespace HealthCare_System.Core.MedicalRecords.Repository
{
    public interface IMedicalRecordRepo
    {
        string Path { get; set; }

        void Add(MedicalRecord medRecord);
        void Delete(MedicalRecord medicalRecord);
        MedicalRecord FindById(int id);
        int GenerateId();
        void Serialize(string linkPath = "../../../data/links/MedicalRecord_Ingredient.csv");
    }
}