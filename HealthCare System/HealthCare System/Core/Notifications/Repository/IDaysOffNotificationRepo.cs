using HealthCare_System.Core.Notifications.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDaysOffNotificationRepo
    {
        string Path { get; set; }

        List<DaysOffNotification> DaysOffNotifications { get; set; }
        DaysOffNotification FindById(int id);
        void Serialize();
    }
}