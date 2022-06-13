using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.MedicalRecords.Repository
{
    public class MedicalRecordRepo
    {
        List<MedicalRecord> medicalRecords;
        string path;
        string ingredientLinkerPath;
        string patientLinkerPath;
        string prescriptionLinkerPath;
        string referralLinkerPath;

        public MedicalRecordRepo()
        {
            path = "../../../data/entities/MedicalRecords.json";
            ingredientLinkerPath = "../../../data/links/MedicalRecord_Ingredient.csv";
            patientLinkerPath = "../../../data/links/MedicalRecord_Patient.csv";
            prescriptionLinkerPath = "../../../data/links/PrescriptionLinker.csv";
            referralLinkerPath = "../../../data/links/ReferralLinker.csv";
            Load();
        }

        public MedicalRecordRepo(string path)
        {
            this.path = path;
            ingredientLinkerPath = "../../../data/links/MedicalRecord_Ingredient.csv";
            patientLinkerPath = "../../../data/links/MedicalRecord_Patient.csv";
            prescriptionLinkerPath = "../../../data/links/PrescriptionLinker.csv";
            referralLinkerPath = "../../../data/links/ReferralLinker.csv";
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

        public void Add(MedicalRecord medRecord)
        {
            medicalRecords.Add(medRecord);
            Serialize();
        }
        public void Delete(MedicalRecord medicalRecord)
        {
            MedicalRecords.Remove(medicalRecord);
            Serialize();
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

        private void RewriteReferralLinker()
        {
            string csv = "";
            foreach (MedicalRecord medRecord in medicalRecords)
            {
                foreach (Referral referral in medRecord.Referrals)
                {
                    string doctorJmbg;
                    if (referral.Doctor is null) doctorJmbg = "-1";
                    else doctorJmbg = referral.Doctor.Jmbg;

                    csv += referral.Id + ";" + doctorJmbg + ";" + medRecord.Id + "\n";
                }
            }
            File.WriteAllText(referralLinkerPath, csv);
        }

        public void Serialize(string linkPath = "../../../data/links/MedicalRecord_Ingredient.csv")
        {
            string medicalRecordsJson = JsonSerializer.Serialize(medicalRecords,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, medicalRecordsJson);

            RewriteIngredientLinker();

            RewritePatientLinker();

            RewritePrescriptionLinker();

            RewriteReferralLinker();
        }

    }
}
