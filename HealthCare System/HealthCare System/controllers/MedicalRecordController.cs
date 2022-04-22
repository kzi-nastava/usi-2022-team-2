using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class MedicalRecordController
    {
        List<MedicalRecord> medicalRecords;

        public MedicalRecordController()
        {
            Load();
        }
        internal List<MedicalRecord> MedicalRecords { get => medicalRecords; set => medicalRecords = value; }

        void Load()
        {
            medicalRecords = JsonSerializer.Deserialize<List<MedicalRecord>>(File.ReadAllText("data/entities/MedicalRecords.json"));
        }

        public MedicalRecord FindById(int id)
        {
            foreach (MedicalRecord medicalRecord in medicalRecords)
                if (medicalRecord.Id == id)
                    return medicalRecord;
            return null;
        }
    }
}
