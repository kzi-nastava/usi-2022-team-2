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
    class SplittingRenovationController
    {
        List<SplittingRenovation> splittingRenovations;

        public SplittingRenovationController()
        {
            this.LoadSplittingRenovations();
        }

        public List<SplittingRenovation> SplittingRenovations
        {
            get { return splittingRenovations; }
            set { splittingRenovations = value; }
        }

        void LoadSplittingRenovations()
        {
            this.splittingRenovations = JsonSerializer.Deserialize<List<SplittingRenovation>>(File.ReadAllText("data/entities/SplittingRenovations.json"));
        }

        public SplittingRenovation FindById(int id)
        {
            foreach (SplittingRenovation splittingRenovation in this.splittingRenovations)
                if (splittingRenovation.Id == id)
                    return splittingRenovation;
            return null;
        }
    }
}
