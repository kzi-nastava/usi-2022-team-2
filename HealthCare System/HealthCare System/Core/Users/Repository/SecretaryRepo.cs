using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Users.Repository
{
    public class SecretaryRepo : ISecretaryRepo
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

        public List<Secretary> Secretaries { get => secretaries; set => secretaries = value; }


        public void Load()
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
