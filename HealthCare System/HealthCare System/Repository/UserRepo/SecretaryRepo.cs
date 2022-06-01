using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Repository.UserRepo
{
    class SecretaryRepo
    {
        List<Secretary> secretaries;
        string path;

        public SecretaryRepo()
        {
            path = "../../../data/entities/Secretaries.json";
            Load();
        }

        public SecretaryRepo(string path)
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
