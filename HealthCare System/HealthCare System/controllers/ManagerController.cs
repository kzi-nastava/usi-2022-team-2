using HealthCare_System.entities;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class ManagerController
    {
        List<Manager> managers;

        public ManagerController()
        {
            this.LoadManagers();
        }

        public List<Manager> Managers
        {
            get { return managers; }
            set { managers = value; }
        }

        void LoadManagers()
        {
            this.managers = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText("data/entities/Managers.json"));
        }

        public Manager FindByMail(string mail)
        {
            foreach (Manager manager in this.managers)
                if (manager.Mail == mail)
                    return manager;
            return null;
        }
    }
}
