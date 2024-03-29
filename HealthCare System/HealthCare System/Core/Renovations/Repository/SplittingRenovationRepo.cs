﻿using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Renovations.Repository
{
    public class SplittingRenovationRepo : ISplittingRenovationRepo
    {
        List<SplittingRenovation> splittingRenovations;
        string path;

        public SplittingRenovationRepo()
        {
            path = "../../../data/entities/SplittingRenovations.json";
            Load();
        }

        public SplittingRenovationRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<SplittingRenovation> SplittingRenovations
        {
            get => splittingRenovations;
            set => splittingRenovations = value;
        }

        public string Path { get => path; set => path = value; }

        public void Load()
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

        public int GenerateId()
        {
            if (splittingRenovations.Count > 0)
            {
                return splittingRenovations[splittingRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv")
        {
            string splittingRenovationsJson = JsonSerializer.Serialize(splittingRenovations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, splittingRenovationsJson);
            string csv = "";
            foreach (SplittingRenovation splittingRenovation in splittingRenovations)
            {
                csv += splittingRenovation.Id + ";" + splittingRenovation.Room.Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }


        public void Add(SplittingRenovation splittingRenovation)
        {
            splittingRenovations.Add(splittingRenovation);
            Serialize();
        }

        public void Delete(SplittingRenovation splittingRenovation)
        {
            splittingRenovations.Remove(splittingRenovation);
            Serialize();
        }


    }
}
