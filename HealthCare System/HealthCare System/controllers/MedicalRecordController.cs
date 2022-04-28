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

        public MedicalRecordController()
        {
            path = "data/entities/MedicalRecords.json";
            Load();
        }

        public MedicalRecordController(string path)
        {
            this.path = path;
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

        public MedicalRecord add(double height, double weight, string diseaseHistory, List<Ingredient> allergens)
        {
            MedicalRecord medRecord = new MedicalRecord(this.GenerateId(), height, weight, diseaseHistory, allergens);
            this.medicalRecords.Add(medRecord);
            return medRecord;
        }

        public int GenerateId()
        {
            return medicalRecords[^1].Id + 1;
        }

        public void Serialize()
        {
            string medicalRecordsJson = JsonSerializer.Serialize(medicalRecords, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, medicalRecordsJson);
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
