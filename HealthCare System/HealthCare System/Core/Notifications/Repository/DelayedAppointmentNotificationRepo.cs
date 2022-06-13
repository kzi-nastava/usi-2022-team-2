using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Notifications.Repository
{
    public class DelayedAppointmentNotificationRepo
    {
        List<DelayedAppointmentNotification> delayedAppointmentNotifications;
        string path;
        string appointmentLinkerPath = "../../../data/links/DelayedAppointmentNotificationLinker.csv";

        public DelayedAppointmentNotificationRepo()
        {
            path = "../../../data/entities/DelayedAppointmentNotifications.json";
            Load();
        }

        public DelayedAppointmentNotificationRepo(string path)
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

        public DelayedAppointmentNotification Add(Appointment appointment, string text)
        {
            DelayedAppointmentNotification newNotification = new DelayedAppointmentNotification(GenerateId(), text, appointment);
            delayedAppointmentNotifications.Add(newNotification);
            return newNotification;
        }

        public int GenerateId()
        {
            return delayedAppointmentNotifications[^1].Id + 1;
        }

        private void RewriteAppointmentLinker()
        {
            string csv = "";
            foreach (DelayedAppointmentNotification notification in delayedAppointmentNotifications)
            {
                if (notification.Appointment is not null)
                {
                    csv += notification.Id + ";" + notification.Appointment.Id + "\n";
                }
            }
            File.WriteAllText(appointmentLinkerPath, csv);
        }

        public void Serialize()
        {
            string delayedAppointmentNotificationsJson = JsonSerializer.Serialize(delayedAppointmentNotifications,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, delayedAppointmentNotificationsJson);

            RewriteAppointmentLinker();
        }
    }
}
