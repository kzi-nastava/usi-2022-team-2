using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DelayedAppointmentNotificationController
    {
        List<DelayedAppointmentNotification> delayedAppointmentNotifications;

        public DelayedAppointmentNotificationController()
        {
            Load();
        }

        public List<DelayedAppointmentNotification> DelayedAppointmentNotifications
        {
            get { return delayedAppointmentNotifications; }
            set { delayedAppointmentNotifications = value; }
        }

        void Load()
        {
            delayedAppointmentNotifications = JsonSerializer.Deserialize<List<DelayedAppointmentNotification>>(File.ReadAllText("data/entities/Drugs.json"));
        }

        public DelayedAppointmentNotification FindById(int id)
        {
            foreach (DelayedAppointmentNotification delayedAppointmentNotification in delayedAppointmentNotifications)
                if (delayedAppointmentNotification.Id == id)
                    return delayedAppointmentNotification;
            return null;
        }
    }
}
