using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DaysOffNotificationController
    {
        List<DaysOffNotification> daysOffNotifications;
        string path;

        public DaysOffNotificationController()
        {
            path = "data/entities/DaysOffNotifications.json";
            Load();
        }

        public DaysOffNotificationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<DaysOffNotification> DaysOffNotifications { get => daysOffNotifications; set => daysOffNotifications = value; }

        public string Path { get => path; set => path = value; }


        void Load()
        {
            daysOffNotifications = JsonSerializer.Deserialize<List<DaysOffNotification>>(File.ReadAllText(path));
        }

        public DaysOffNotification FindById(int id)
        {
            foreach (DaysOffNotification daysOffNotification in daysOffNotifications)
                if (daysOffNotification.Id == id)
                    return daysOffNotification;
            return null;
        }

        public void Serialize()
        {
            string daysOffNotificationsJson = JsonSerializer.Serialize(daysOffNotifications, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, daysOffNotificationsJson);
        }
    }
}
