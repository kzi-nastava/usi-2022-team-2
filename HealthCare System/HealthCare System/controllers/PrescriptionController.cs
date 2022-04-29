using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class PrescriptionController
    {
        List<Prescription> prescriptions;
        string path;

        public PrescriptionController()
        {
            path = "../../../data/entities/Prescriptions.json";
            Load();
        }

        public PrescriptionController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Prescription> Prescriptions { get => prescriptions; set => prescriptions = value; }

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

        public void Serialize()
        {
            string prescriptionsJson = JsonSerializer.Serialize(prescriptions,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, prescriptionsJson);
        }
    }
}
