using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;

namespace HealthCare_System.Core.Notifications.Repository
{
    public class DaysOffNotificationRepo : IDaysOffNotificationRepo
    {
        List<DaysOffNotification> daysOffNotifications;
        string path;

        public DaysOffNotificationRepo()
        {
            path = "../../../data/entities/DaysOffNotifications.json";
            Load();
        }

        public DaysOffNotificationRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<DaysOffNotification> DaysOffNotifications { get => daysOffNotifications; set => daysOffNotifications = value; }

        public string Path { get => path; set => path = value; }


        void Load()
        {
            daysOffNotifications = JsonSerializer.Deserialize<List<DaysOffNotification>>(File.ReadAllText(path));
        }

        public void Add(Doctor doctor, string message)
        {
            daysOffNotifications.Add(new DaysOffNotification(GenerateId(), message, doctor));
            Serialize();
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

        public int GenerateId()
        {
            if (daysOffNotifications.Count == 0)
            {
                return 1001;
            }
            return daysOffNotifications[^1].Id + 1;
        }
    }
}
