using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class SplittingRenovationController
    {
        List<SplittingRenovation> splittingRenovations;
        string path;

        public SplittingRenovationController()
        {
            path = "../../../data/entities/SplittingRenovations.json";
            Load();
        }

        public SplittingRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<SplittingRenovation> SplittingRenovations { get => splittingRenovations; 
            set => splittingRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            splittingRenovations = JsonSerializer.Deserialize<List<SplittingRenovation>>(File.ReadAllText(path));
        }

        public SplittingRenovation FindById(int id)
        {
            foreach (SplittingRenovation splittingRenovation in splittingRenovations)
                if (splittingRenovation.Id == id)
                    return splittingRenovation;
            return null;
        }

        public void Serialize()
        {
            string splittingRenovationsJson = JsonSerializer.Serialize(splittingRenovations, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, splittingRenovationsJson);
        }
    }
}
