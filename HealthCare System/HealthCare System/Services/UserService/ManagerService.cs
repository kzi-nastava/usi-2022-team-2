using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Services.UserService
{
    class ManagerService
    {
        List<Manager> managers;
        string path;

        public ManagerService()
        {
            path = "../../../data/entities/Managers.json";
            Load();
        }

        public ManagerService(string path)
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
