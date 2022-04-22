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
    class SimpleRenovationController
    {
        List<SimpleRenovation> simpleRenovations;

        public SimpleRenovationController()
        {
            this.LoadSimpleRenovations();
        }

        public List<SimpleRenovation> SimpleRenovations
        {
            get { return simpleRenovations; }
            set { simpleRenovations = value; }
        }

        void LoadSimpleRenovations() 
        {
            this.simpleRenovations = JsonSerializer.Deserialize<List<SimpleRenovation>>(File.ReadAllText("data/entities/SimpleRenovations.json"));
        }

        public SimpleRenovation FindById(int id)
        {
            foreach (SimpleRenovation simpleRenovation in this.simpleRenovations)
                if (simpleRenovation.Id == id)
                    return simpleRenovation;
            return null;
        }

    }
}
