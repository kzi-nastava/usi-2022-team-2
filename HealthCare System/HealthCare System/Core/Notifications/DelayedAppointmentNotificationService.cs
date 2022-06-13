using System;
using System.Collections.Generic;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Notifications.Repository;

namespace HealthCare_System.Core.Notifications
{
    public class DelayedAppointmentNotificationService : IDelayedAppointmentNotificationService
    {
        DelayedAppointmentNotificationRepo delayedAppointmentNotificationRepo;

        public DelayedAppointmentNotificationService(DelayedAppointmentNotificationRepo delayedAppointmentNotificationRepo)
        {
            this.delayedAppointmentNotificationRepo = delayedAppointmentNotificationRepo;
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
