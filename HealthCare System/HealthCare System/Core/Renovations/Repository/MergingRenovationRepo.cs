using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Renovations.Repository
{
    public class MergingRenovationRepo : IMergingRenovationRepo
    {
        List<MergingRenovation> mergingRenovations;
        string path;

        public MergingRenovationRepo()
        {
            path = "../../../data/entities/MergingRenovations.json";
            Load();
        }

        public MergingRenovationRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<MergingRenovation> MergingRenovations { get => mergingRenovations; set => mergingRenovations = value; }

        public string Path { get => path; set => path = value; }

        public void Load()
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

        public int GenerateId()
        {
            if (mergingRenovations.Count > 0)
            {
                return mergingRenovations[mergingRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv")
        {
            string mergingRenovationsJson = JsonSerializer.Serialize(mergingRenovations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, mergingRenovationsJson);
            string csv = "";
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
            {
                csv += mergingRenovation.Id + ";" + mergingRenovation.Rooms[0].Id + ";"
                    + mergingRenovation.Rooms[1].Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        public void Add(MergingRenovation mergingRenovation)
        {
            mergingRenovations.Add(mergingRenovation);
            Serialize();
        }

        public void Delete(MergingRenovation mergingRenovation)
        {
            mergingRenovations.Remove(mergingRenovation);
            Serialize();
        }
    }
}
