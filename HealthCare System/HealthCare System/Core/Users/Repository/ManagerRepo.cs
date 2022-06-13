using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Users.Repository
{
    public class ManagerRepo
    {
                List<Manager> managers;
        string path;

        public ManagerRepo()
        {
            path = "../../../data/entities/Managers.json";
            Load();
        }

        public ManagerRepo(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Manager> Managers { get => managers; set => managers = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            managers = JsonSerializer.Deserialize<List<Manager>>(File.ReadAllText(path));
        }

        public Manager FindByMail(string mail)
        {
            foreach (Manager manager in managers)
                if (manager.Mail == mail)
                    return manager;
            return null;
        }

        public void Serialize()
        {
            string managersJson = JsonSerializer.Serialize(managers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, managersJson);
        }
    }
}
