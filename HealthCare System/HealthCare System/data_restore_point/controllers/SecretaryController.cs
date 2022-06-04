using HealthCare_System.Model;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace HealthCare_System.controllers
{
    class SecretaryController
    {
        List<Secretary> secretaries;
        string path;

        public SecretaryController()
        {
            path = "../../../data/entities/Secretaries.json";
            Load();
        }

        public SecretaryController(string path)
        {
            this.path = path;
            Load();
        }

        public string Path { get => path; set => path = value; }

        internal List<Secretary> Secretaries { get => secretaries; set => secretaries = value; }


        void Load()
        {
            secretaries = JsonSerializer.Deserialize<List<Secretary>>(File.ReadAllText(this.path));
        }

        public void Serialize()
        {
            string secretaryJson = JsonSerializer.Serialize(secretaries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, secretaryJson);
        }
    }
}
