using System.Collections.Generic;
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
            Load();
        }

        public List<MergingRenovation> MergingRenovations
        {
            get { return mergingRenovations; }
            set => mergingRenovations = value;
        }

        void Load()
        {
            mergingRenovations = JsonSerializer.Deserialize<List<MergingRenovation>>(File.ReadAllText("data/entities/MergingRenovations.json"));
        }

        public MergingRenovation FindById(int id)
        {
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
                if (mergingRenovation.Id == id)
                    return mergingRenovation;
            return null;
        }
    }
}
