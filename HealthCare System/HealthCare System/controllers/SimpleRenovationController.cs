using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class SimpleRenovationController
    {
        List<SimpleRenovation> simpleRenovations;
        string path;

        public SimpleRenovationController()
        {
            path = "../../../data/entities/SimpleRenovations.json";
            Load();
        }

        public SimpleRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<SimpleRenovation> SimpleRenovations { get => simpleRenovations; set => simpleRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load() 
        {
            simpleRenovations = JsonSerializer.Deserialize<List<SimpleRenovation>>(File.ReadAllText(path));
        }

        public SimpleRenovation FindById(int id)
        {
            foreach (SimpleRenovation simpleRenovation in simpleRenovations)
                if (simpleRenovation.Id == id)
                    return simpleRenovation;
            return null;
        }

        public void Serialize()
        {
            string simpleRenovationsJson = JsonSerializer.Serialize(simpleRenovations, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, simpleRenovationsJson);
        }
    }
}
