using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DelayedAppointmentNotificationController
    {
        List<DelayedAppointmentNotification> delayedAppointmentNotifications;
        string path;

        public DelayedAppointmentNotificationController()
        {
            path = "../../../data/entities/Drugs.json";
            Load();
        }

        public DelayedAppointmentNotificationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<DelayedAppointmentNotification> DelayedAppointmentNotifications 
        { get => delayedAppointmentNotifications; set => delayedAppointmentNotifications = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            delayedAppointmentNotifications = JsonSerializer.
                Deserialize<List<DelayedAppointmentNotification>>(File.ReadAllText(path));
        }

        public DelayedAppointmentNotification FindById(int id)
        {
            foreach (DelayedAppointmentNotification delayedAppointmentNotification in delayedAppointmentNotifications)
                if (delayedAppointmentNotification.Id == id)
                    return delayedAppointmentNotification;
            return null;
        }

        public void Serialize()
        {
            string delayedAppointmentNotificationsJson = JsonSerializer.Serialize(delayedAppointmentNotifications,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, delayedAppointmentNotificationsJson);
        }
    }
}
