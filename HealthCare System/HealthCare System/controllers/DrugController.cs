using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class DrugController
    {
        List<Drug> drugs;

        public DrugController()
        {
            this.LoadDrugs();
        }

        public List<Drug> Drugs
        {
            get { return drugs; }
            set { drugs = value; }
        }

        void LoadDrugs()
        {
            this.drugs = JsonSerializer.Deserialize<List<Drug>>(File.ReadAllText("data/entities/Drugs.json"));
        }

        public Drug FindById(int id)
        {
            foreach (Drug drug in this.drugs)
                if (drug.Id == id)
                    return drug;
            return null;
        }
    }
}
