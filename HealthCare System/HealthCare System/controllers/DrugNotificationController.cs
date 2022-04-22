using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class DrugNotificationController
    {
        List<DrugNotification> drugNotifications;

        public DrugNotificationController()
        {
            this.LoadDrugNotifications();
        }

        public List<DrugNotification> DrugNotifications
        {
            get { return drugNotifications; }
            set { drugNotifications = value; }
        }

        void LoadDrugNotifications()
        {
            this.drugNotifications = JsonSerializer.Deserialize<List<DrugNotification>>(File.ReadAllText("data/entities/DrugNotifications.json"));
        }

        public DrugNotification FindById(int id)
        {
            foreach (DrugNotification drugNofiticaion in this.drugNotifications)
                if (drugNofiticaion.Id == id)
                    return drugNofiticaion;
            return null;
        }
    }
}
