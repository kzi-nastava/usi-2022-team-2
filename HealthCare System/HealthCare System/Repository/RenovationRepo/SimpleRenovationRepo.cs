﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Repository.RenovationRepo
{
    public class SimpleRenovationRepo
    {
        List<SimpleRenovation> simpleRenovations;
        string path;

        public SimpleRenovationRepo()
        {
            path = "../../../data/entities/SimpleRenovations.json";
            Load();
        }

        public SimpleRenovationRepo(string path)
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

        public int GenerateId()
        {
            if (simpleRenovations.Count > 0)
            {
                return simpleRenovations[simpleRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv")
        {
            string simpleRenovationsJson = JsonSerializer.Serialize(simpleRenovations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, simpleRenovationsJson);
            string csv = "";
            foreach (SimpleRenovation simpleRenovation in simpleRenovations)
            {
                csv += simpleRenovation.Id + ";" + simpleRenovation.Room.Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        public void Add(SimpleRenovation simpleRenovation)
        {
            simpleRenovations.Add(simpleRenovation);
            Serialize();
        }

        public void Delete(SimpleRenovation simpleRenovation)
        {
            simpleRenovations.Remove(simpleRenovation);
            Serialize();
        }

    }
}
