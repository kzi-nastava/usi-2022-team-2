﻿using HealthCare_System.Core.Prescriptions.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Prescriptions.Repository
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        List<Prescription> prescriptions;
        string path;

        public PrescriptionRepo()
        {
            path = "../../../data/entities/Prescriptions.json";
            Load();
        }

        public PrescriptionRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<Prescription> Prescriptions { get => prescriptions; set => prescriptions = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            prescriptions = JsonSerializer.Deserialize<List<Prescription>>(File.ReadAllText(path));
        }

        public Prescription FindById(int id)
        {
            foreach (Prescription prescription in prescriptions)
                if (prescription.Id == id)
                    return prescription;
            return null;
        }

        public int GenerateId()
        {
            return prescriptions[^1].Id + 1;
        }

        public void Serialize(string linkPath = "../../../data/links/PrescriptionLinker.csv")
        {
            string prescriptionsJson = JsonSerializer.Serialize(prescriptions,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, prescriptionsJson);

            string csv = "";
            foreach (Prescription prescription in prescriptions)
            {
                csv += prescription.Id + ";" + prescription.MedicalRecord.Id + ";" + prescription.Drug.Id + "\n";
            }

            File.WriteAllText(linkPath, csv);
        }

        public void Add(Prescription prescription)
        {
            prescriptions.Add(prescription);
            Serialize();
        }

        public void Delete(Prescription prescription)
        {
            prescriptions.Remove(prescription);
            Serialize();
        }
    }
}
