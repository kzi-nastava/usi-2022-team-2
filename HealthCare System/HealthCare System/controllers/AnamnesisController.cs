using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class AnamnesisController
    {
        List<Anamnesis> anamneses;

        public AnamnesisController()
        {
            Load();
        }

        internal List<Anamnesis> Anamneses { get => anamneses; set => anamneses = value; }

        void Load()
        {
            anamneses = JsonSerializer.Deserialize<List<Anamnesis>>(File.ReadAllText("data/entities/Anamneses.json"));
        }

        public Anamnesis FindById(int id)
        {
            foreach (Anamnesis anamnesis in anamneses)
                if (anamnesis.Id == id)
                    return anamnesis;
            return null;
        }
    }
}
