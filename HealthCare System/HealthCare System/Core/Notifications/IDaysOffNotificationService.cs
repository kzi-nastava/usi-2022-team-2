using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications
{
    public interface IDaysOffNotificationService
    {
        void AddDaysOffNotification(Doctor doctor, string message);

        public List<DaysOffNotification> DaysOffNotifications();
    }
}