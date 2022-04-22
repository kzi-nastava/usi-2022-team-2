using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DaysOffNotificationController
    {
        List<DaysOffNotification> daysOffNotifications;

        public DaysOffNotificationController()
        {
            Load();
        }

        public List<DaysOffNotification> Drugs
        {
            get { return daysOffNotifications; }
            set { daysOffNotifications = value; }
        }

        void Load()
        {
            daysOffNotifications = JsonSerializer.Deserialize<List<DaysOffNotification>>(File.ReadAllText("data/entities/DaysOffNotificationss.json"));
        }

        public DaysOffNotification FindById(int id)
        {
            foreach (DaysOffNotification daysOffNotification in daysOffNotifications)
                if (daysOffNotification.Id == id)
                    return daysOffNotification;
            return null;
        }
    }
}
