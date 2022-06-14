using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Notifications.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.gui.Controller
{
    class DelayedAppointmentNotificationController
    {
        private readonly IDelayedAppointmentNotificationService delayedAppointmentNotificationService;

        public DelayedAppointmentNotificationController(IDelayedAppointmentNotificationService delayedAppointmentNotificationService)
        {
            this.delayedAppointmentNotificationService = delayedAppointmentNotificationService;
        }

        public List<DelayedAppointmentNotification> DelayedAppointmentNotifications()
        {
            return delayedAppointmentNotificationService.DelayedAppointmentNotifications();
        }

        public void AddNotification(Appointment appointment, DateTime oldStart)
        {
            delayedAppointmentNotificationService.AddNotification(appointment, oldStart);
        }

        public DelayedAppointmentNotification FindById(int id)
        {
            return delayedAppointmentNotificationService.FindById(id);
        }

        public int GenerateId()
        {
            return delayedAppointmentNotificationService.GenerateId();
        }


        public void Serialize()
        {
            delayedAppointmentNotificationService.Serialize();
        }
    }
}
