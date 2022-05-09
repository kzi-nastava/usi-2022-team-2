using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class MedicalRecordController
    {
        List<MedicalRecord> medicalRecords;
        string path;
        string ingredientLinkerPath;
        string patientLinkerPath;
        string prescriptionLinkerPath;

        public MedicalRecordController()
        {
            path = "../../../data/entities/MedicalRecords.json";
            ingredientLinkerPath = "../../../data/links/MedicalRecord_Ingredient.csv";
            patientLinkerPath = "../../../data/links/MedicalRecord_Patient.csv";
            prescriptionLinkerPath = "../../../data/links/PrescriptionLinker.csv";
            Load();
        }

        public MedicalRecordController(string path)
        {
            this.path = path;
            ingredientLinkerPath = "../../../data/links/MedicalRecord_Ingredient.csv";
            patientLinkerPath = "../../../data/links/MedicalRecord_Patient.csv";
            prescriptionLinkerPath = "../../../data/links/PrescriptionLinker.csv";
            Load();
        }

        internal List<MedicalRecord> MedicalRecords { get => medicalRecords; set => medicalRecords = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            medicalRecords = JsonSerializer.Deserialize<List<MedicalRecord>>(File.ReadAllText(path));
        }

        public MedicalRecord FindById(int id)
        {
            foreach (MedicalRecord medicalRecord in medicalRecords)
                if (medicalRecord.Id == id)
                    return medicalRecord;
            return null;
        }

        public MedicalRecord Add(double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            MedicalRecord medRecord = new(this.GenerateId(), height, weight, diseaseHistory, allergens);
            medicalRecords.Add(medRecord);
            return medRecord;
        }

        public int GenerateId()
        {
            return medicalRecords[^1].Id + 1;
        }

        private void RewriteIngredientLinker()
        {
            string csv = "";
            foreach (MedicalRecord medRecrod in medicalRecords)
            {
                foreach (Ingredient ingredient in medRecrod.Allergens)
                {
                    csv += medRecrod.Id + ";" + ingredient.Id + "\n";
                }
            }
            File.WriteAllText(ingredientLinkerPath, csv);
        }

        private void RewritePatientLinker()
        {
            string csv = "";
            foreach (MedicalRecord medRecord in medicalRecords)
            {
                csv += medRecord.Id + ";" + medRecord.Patient.Jmbg + "\n";
            }
            File.WriteAllText(patientLinkerPath, csv);
        }

        private void RewritePrescriptionLinker()
        {
            string csv = "";
            foreach (MedicalRecord medRecord in medicalRecords)
            {
                foreach (Prescription prescription in medRecord.Prescriptions)
                {
                    csv += prescription.Id + ";" + medRecord.Id + ";" + prescription.Drug.Id + "\n";
                }
            }
            File.WriteAllText(prescriptionLinkerPath, csv);
        }

        public void Serialize(string linkPath = "../../../data/links/MedicalRecord_Ingredient.csv")
        {
            string medicalRecordsJson = JsonSerializer.Serialize(medicalRecords, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, medicalRecordsJson);

            RewriteIngredientLinker();

            RewritePatientLinker();

            RewritePrescriptionLinker();
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
