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

        public void Serialize()
        {
            string medicalRecordsJson = JsonSerializer.Serialize(medicalRecords, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, medicalRecordsJson);
        }
    }
}
