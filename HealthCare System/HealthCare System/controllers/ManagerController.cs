using HealthCare_System.entities;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace HealthCare_System.controllers
{
    class ManagerController
    {
        List<Manager> managers;

        public ManagerController()
        {
            Load();
        }

        public List<Manager> Managers
        {
            get { return managers; }
            set { managers = value; }
        }

        void Load()
        {
            managers = JsonSerializer.Deserialize<List<Manager>>(File.ReadAllText("data/entities/Managers.json"));
        }

        public Manager FindByMail(string mail)
        {
            foreach (Manager manager in managers)
                if (manager.Mail == mail)
                    return manager;
            return null;
        }
    }
}
