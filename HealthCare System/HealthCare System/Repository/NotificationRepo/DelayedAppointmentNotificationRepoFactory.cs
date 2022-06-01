using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.NotificationRepo
{
    class DelayedAppointmentNotificationRepoFactory
    {
        private DelayedAppointmentNotificationRepo delayedAppointmentNotificationRepo;

        public DelayedAppointmentNotificationRepo CreateDelayedAppointmentNotificationRepository()
        {
            if (delayedAppointmentNotificationRepo == null)
                delayedAppointmentNotificationRepo = new DelayedAppointmentNotificationRepo();

            return delayedAppointmentNotificationRepo;
        }
    }
}
