using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DrugController
    {
        List<Drug> drugs;

        public DrugController()
        {
            Load();
        }

        public List<Drug> Drugs
        {
            get { return drugs; }
            set { drugs = value; }
        }

        void Load()
        {
            drugs = JsonSerializer.Deserialize<List<Drug>>(File.ReadAllText("data/entities/Drugs.json"));
        }

        public Drug FindById(int id)
        {
            foreach (Drug drug in drugs)
                if (drug.Id == id)
                    return drug;
            return null;
        }
    }
}
