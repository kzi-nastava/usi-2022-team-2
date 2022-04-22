using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class SimpleRenovationController
    {
        List<SimpleRenovation> simpleRenovations;

        public SimpleRenovationController()
        {
            Load();
        }

        public List<SimpleRenovation> SimpleRenovations
        {
            get { return simpleRenovations; }
            set { simpleRenovations = value; }
        }

        void Load() 
        {
            simpleRenovations = JsonSerializer.Deserialize<List<SimpleRenovation>>(File.ReadAllText("data/entities/SimpleRenovations.json"));
        }

        public SimpleRenovation FindById(int id)
        {
            foreach (SimpleRenovation simpleRenovation in simpleRenovations)
                if (simpleRenovation.Id == id)
                    return simpleRenovation;
            return null;
        }

    }
}
