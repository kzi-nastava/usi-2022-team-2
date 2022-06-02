using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.NotificationRepo;
using HealthCare_System.Model;

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

        public List<DelayedAppointmentNotification> DelayedAppointmentNotifications()
        {
            return delayedAppointmentNotificationRepo.DelayedAppointmentNotifications;
        }

        public DelayedAppointmentNotificationRepo DelayedAppointmentNotificationRepo 
            { get => delayedAppointmentNotificationRepo; }

        public void AddNotification(Appointment appointment, DateTime oldStart)
        {
            string text = "Your appointment booked for " + oldStart + " is delayed. New start is on: " + appointment.Start + ".";
            DelayedAppointmentNotification newNotification = delayedAppointmentNotificationRepo.Add(appointment, text);
            delayedAppointmentNotificationRepo.Serialize();// proveriti da li vam ova metoda treba ovde ili u add poso ja nisam radio

        }
    }
}
