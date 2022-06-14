using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.MedicalRecords
{
    class MedicalRecordController
    {
        private readonly IMedicalRecordService medicalRecorService;

        public MedicalRecordController(IMedicalRecordService medicalRecorService)
        {
            this.medicalRecorService = medicalRecorService;
        }

        public MedicalRecord Add(double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            return medicalRecorService.Add(height, weight, diseaseHistory, allergens);
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            medicalRecorService.Delete(medicalRecord);
        }

        public MedicalRecord FindById(int id)
        {
            return medicalRecorService.FindById(id);
        }

        public List<MedicalRecord> MedicalRecords()
        {
            return medicalRecorService.MedicalRecords();
        }

        public void Update(int id, double height, double weight, string diseaseHistory)
        {
            medicalRecorService.Update(id, height, weight, diseaseHistory);
        }

        public int GenerateId()
        {
            return medicalRecorService.GenerateId();
        }

        public void Serialize()
        {
            medicalRecorService.Serialize();
        }
    }
}
