using System.Collections.Generic;
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
            Load();
        }

        public List<SplittingRenovation> SplittingRenovations
        {
            get { return splittingRenovations; }
            set { splittingRenovations = value; }
        }

        void Load()
        {
            splittingRenovations = JsonSerializer.Deserialize<List<SplittingRenovation>>(File.ReadAllText("data/entities/SplittingRenovations.json"));
        }

        public SplittingRenovation FindById(int id)
        {
            foreach (SplittingRenovation splittingRenovation in splittingRenovations)
                if (splittingRenovation.Id == id)
                    return splittingRenovation;
            return null;
        }
    }
}
