using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;

namespace HealthCare_System.Core.Drugs.Repository
{
    public class DrugRepo : IDrugRepo
    {
        List<Drug> drugs;
        string path;

        public DrugRepo()
        {
            path = "../../../data/entities/Drugs.json";
            Load();
        }

        public DrugRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<Drug> Drugs { get => drugs; set => drugs = value; }

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

        public int GenerateId()
        {
            if (drugs.Count == 0)
                return 1001;
            return drugs[^1].Id + 1;
        }

        public void Serialize(string linkPath = "../../../data/links/Drug_Ingredient.csv")
        {
            string drugsJson = JsonSerializer.Serialize(drugs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, drugsJson);
            string csv = "";
            foreach (Drug drug in drugs)
            {
                foreach (Ingredient ingredient in drug.Ingredients)
                {
                    csv += drug.Id.ToString() + ";" + ingredient.Id.ToString() + "\n";
                }

            }
            File.WriteAllText(linkPath, csv);
        }

        public void Add(Drug drug)
        {
            drugs.Add(drug);
            Serialize();
        }

        public void Delete(Drug drug)
        {
            drugs.Remove(drug);
            Serialize();
        }

        public List<Drug> FillterOnHold()
        {
            List<Drug> filtered = new List<Drug>();

            foreach (Drug drug in drugs)
                if (drug.Status == DrugStatus.ON_HOLD)
                    filtered.Add(drug);

            return filtered;
        }

        public List<Drug> FillterAccepted()
        {
            List<Drug> filtered = new List<Drug>();

            foreach (Drug drug in drugs)
                if (drug.Status == DrugStatus.ACCEPTED)
                    filtered.Add(drug);

            return filtered;
        }
    }
}
