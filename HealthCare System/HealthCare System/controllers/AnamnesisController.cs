using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class AnamnesisController
    {
        List<Anamnesis> anamneses;
        string path;

        public AnamnesisController()
        {
            path = "../../../data/entities/Anamneses.json";
            Load();
        }

        public AnamnesisController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Anamnesis> Anamneses { get => anamneses; set => anamneses = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            anamneses = JsonSerializer.Deserialize<List<Anamnesis>>(File.ReadAllText(path));
        }

        public Anamnesis FindById(int id)
        {
            foreach (Anamnesis anamnesis in anamneses)
                if (anamnesis.Id == id)
                    return anamnesis;
            return null;
        }

        public void Serialize()
        {
            string anamnesesJson = JsonSerializer.Serialize(anamneses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, anamnesesJson);
        }

        public int GenerateId()
        {
            return anamneses[^1].Id + 1;
        }

        public void UpdateAnamnesis(int id, string description)
        {
            Anamnesis anamnesis = FindById(id);
            anamnesis.Description = description;
            Serialize();
        }

        
    }
}
