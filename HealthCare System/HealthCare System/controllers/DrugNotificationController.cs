using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DrugNotificationController
    {
        List<DrugNotification> drugNotifications;

        public DrugNotificationController()
        {
            Load();
        }

        public List<DrugNotification> DrugNotifications
        {
            get { return drugNotifications; }
            set { drugNotifications = value; }
        }

        void Load()
        {
            drugNotifications = JsonSerializer.Deserialize<List<DrugNotification>>(File.ReadAllText("data/entities/DrugNotifications.json"));
        }

        public DrugNotification FindById(int id)
        {
            foreach (DrugNotification drugNofiticaion in drugNotifications)
                if (drugNofiticaion.Id == id)
                    return drugNofiticaion;
            return null;
        }
    }
}
