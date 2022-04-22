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
    class DelayedAppointmentNotificationController
    {
        List<DelayedAppointmentNotification> delayedAppointmentNotifications;

        public DelayedAppointmentNotificationController()
        {
            this.LoadDelayedAppointmentNotifications();
        }

        public List<DelayedAppointmentNotification> DelayedAppointmentNotifications
        {
            get { return delayedAppointmentNotifications; }
            set { delayedAppointmentNotifications = value; }
        }

        void LoadDelayedAppointmentNotifications()
        {
            this.delayedAppointmentNotifications = JsonSerializer.Deserialize<List<DelayedAppointmentNotification>>(File.ReadAllText("data/entities/Drugs.json"));
        }

        public DelayedAppointmentNotification FindById(int id)
        {
            foreach (DelayedAppointmentNotification delayedAppointmentNotification in this.delayedAppointmentNotifications)
                if (delayedAppointmentNotification.Id == id)
                    return delayedAppointmentNotification;
            return null;
        }
    }
}
