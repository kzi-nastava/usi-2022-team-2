using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DrugController
    {
        List<Drug> drugs;
        string path;

        public DrugController()
        {
            path = "../../../data/entities/Drugs.json";
            Load();
        }

        public DrugController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Drug> Drugs { get => drugs; set => drugs = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            drugs = JsonSerializer.Deserialize<List<Drug>>(File.ReadAllText(path));
        }

        public Drug FindById(int id)
        {
            foreach (Drug drug in drugs)
                if (drug.Id == id)
                    return drug;
            return null;
        }

        public void Serialize()
        {
            string drugsJson = JsonSerializer.Serialize(drugs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, drugsJson);
        }
    }
}
