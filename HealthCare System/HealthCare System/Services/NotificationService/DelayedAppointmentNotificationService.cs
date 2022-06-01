using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.NotificationRepo;

namespace HealthCare_System.Services.NotificationService
{
    class DelayedAppointmentNotificationService
    {
        DelayedAppointmentNotificationRepo delayedAppointmentNotificationRepo;

        public DelayedAppointmentNotificationService()
        {
            DelayedAppointmentNotificationRepoFactory delayedAppointmentNotificationRepoFactory = new();
            delayedAppointmentNotificationRepo = 
                delayedAppointmentNotificationRepoFactory.CreateDelayedAppointmentNotificationRepository();
        }

        public DelayedAppointmentNotificationRepo DelayedAppointmentNotificationRepo 
            { get => delayedAppointmentNotificationRepo; }

        public void AddNotification(Appointment appointment, DateTime oldStart)
        {
            string text = "Your appointment booked for " + oldStart + " is delayed. New start is on: " + appointment.Start + ".";
            DelayedAppointmentNotification newNotification = delayedAppointmentNotificationController.Add(appointment, text);
            delayedAppointmentNotificationController.Serialize();
        }
    }
}
