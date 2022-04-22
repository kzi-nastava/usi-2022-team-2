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
    class DaysOffNotificationController
    {
        List<DaysOffNotification> daysOffNotifications;

        public DaysOffNotificationController()
        {
            this.LoadDaysOffNotifications();
        }

        public List<DaysOffNotification> Drugs
        {
            get { return daysOffNotifications; }
            set { daysOffNotifications = value; }
        }

        void LoadDaysOffNotifications()
        {
            this.daysOffNotifications = JsonSerializer.Deserialize<List<DaysOffNotification>>(File.ReadAllText("data/entities/DaysOffNotificationss.json"));
        }

        public DaysOffNotification FindById(int id)
        {
            foreach (DaysOffNotification daysOffNotification in this.daysOffNotifications)
                if (daysOffNotification.Id == id)
                    return daysOffNotification;
            return null;
        }
    }
}
