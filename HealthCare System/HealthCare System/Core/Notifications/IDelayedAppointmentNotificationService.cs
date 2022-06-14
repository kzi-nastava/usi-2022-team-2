using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Notifications.Repository;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications
{
    public interface IDelayedAppointmentNotificationService
    {
        IDelayedAppointmentNotificationRepo DelayedAppointmentNotificationRepo { get; }

        void AddNotification(Appointment appointment, DateTime oldStart);
        List<DelayedAppointmentNotification> DelayedAppointmentNotifications();
    }
}