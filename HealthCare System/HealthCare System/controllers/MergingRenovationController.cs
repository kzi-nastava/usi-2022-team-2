using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class MergingRenovationController
    {
        List<MergingRenovation> mergingRenovations;
        string path;

        public MergingRenovationController()
        {
            path = "../../../data/entities/MergingRenovations.json";
            Load();
        }

        public MergingRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<MergingRenovation> MergingRenovations { get => mergingRenovations; set => mergingRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            mergingRenovations = JsonSerializer.Deserialize<List<MergingRenovation>>(File.ReadAllText(path));
        }

        public MergingRenovation FindById(int id)
        {
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
                if (mergingRenovation.Id == id)
                    return mergingRenovation;
            return null;
        }

        public void Serialize()
        {
            string mergingRenovationsJson = JsonSerializer.Serialize(mergingRenovations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, mergingRenovationsJson);
        }
    }
}
