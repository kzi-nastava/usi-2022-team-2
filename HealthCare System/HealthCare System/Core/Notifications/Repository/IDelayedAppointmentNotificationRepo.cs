using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Notifications.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDelayedAppointmentNotificationRepo
    {
        string Path { get; set; }

        List<DelayedAppointmentNotification> DelayedAppointmentNotifications
        { get; set; }
        DelayedAppointmentNotification Add(Appointment appointment, string text);
        DelayedAppointmentNotification FindById(int id);
        int GenerateId();
        void Serialize();
    }
}