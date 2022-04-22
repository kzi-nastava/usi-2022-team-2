using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class MergingRenovationController
    {
        List<MergingRenovation> mergingRenovations;

        public MergingRenovationController()
        {
            this.LoadMergingRenovations();
        }

        public List<MergingRenovation> MergingRenovations
        {
            get { return mergingRenovations; }
            set { mergingRenovations = value; }
        }

        void LoadMergingRenovations()
        {
            this.mergingRenovations = JsonSerializer.Deserialize<List<MergingRenovation>>(File.ReadAllText("data/entities/MergingRenovations.json"));
        }

        public MergingRenovation FindById(int id)
        {
            foreach (MergingRenovation mergingRenovation in this.mergingRenovations)
                if (mergingRenovation.Id == id)
                    return mergingRenovation;
            return null;
        }
    }
}
