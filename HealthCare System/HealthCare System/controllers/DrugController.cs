using HealthCare_System.entities;
using System;
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
                    csv += drug.Id.ToString() + ";" + ingredient.Id.ToString() +  "\n";
                }

            }
            File.WriteAllText(linkPath, csv);
        }

        public void CreateNewDrug(string name, List<Ingredient> ingredients)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Drug drug = new Drug(GenerateId(), name, ingredients, DrugStatus.ON_HOLD, "");
            drugs.Add(drug);
            Serialize();
        }

        public void UpdateDrug(string name, List<Ingredient> ingredients, Drug drug)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            drug.Name = name;
            drug.Ingredients = ingredients;
            drug.Status = DrugStatus.ON_HOLD;
            drug.Message = "";
            Serialize();
        }

        public void RejectDrug(Drug drug, string message)
        {
            drug.Status = DrugStatus.REJECTED;
            drug.Message = message;
            Serialize();
        }

        public void AcceptDrug(Drug drug)
        {
            drug.Status = DrugStatus.ACCEPTED;
            drug.Message = "";
            Serialize();
        }

        public void DeleteDrug(Drug drug)
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
    }
}
