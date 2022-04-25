using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DrugNotificationController
    {
        List<DrugNotification> drugNotifications;
        string path;

        public DrugNotificationController()
        {
            path = "data/entities/DrugNotifications.json";
            Load();
        }

        public DrugNotificationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<DrugNotification> DrugNotifications { get => drugNotifications; set => drugNotifications = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            drugNotifications = JsonSerializer.Deserialize<List<DrugNotification>>(File.ReadAllText(path));
        }

        public DrugNotification FindById(int id)
        {
            foreach (DrugNotification drugNofiticaion in drugNotifications)
                if (drugNofiticaion.Id == id)
                    return drugNofiticaion;
            return null;
        }

        public void Serialize()
        {
            string drugNotificationsJson = JsonSerializer.Serialize(drugNotifications, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, drugNotificationsJson);
        }
    }
}
