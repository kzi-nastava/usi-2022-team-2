using HealthCare_System.entities;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace HealthCare_System.controllers
{
    class ManagerController
    {
        List<Manager> managers;
        string path;

        public ManagerController()
        {
            path = "data/entities/Managers.json";
            Load();
        }

        public ManagerController(string path)
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
