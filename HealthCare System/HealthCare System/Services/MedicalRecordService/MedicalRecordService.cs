﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.MedicalRecordRepo;

namespace HealthCare_System.Services.MedicalRecordService
{
    class MedicalRecordService
    {
        MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService()
        {
            MedicalRecordRepoFactory medicalRecordRepoFactory = new();
            medicalRecordRepo = medicalRecordRepoFactory.CreateMedicalRecordRepository();
        }

        public MedicalRecordRepo MedicalRecordRepo { get => medicalRecordRepo; }

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