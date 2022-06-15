using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Notifications.Repository;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications
{
    public interface IDrugNotificationService
    {
        IDrugNotificationRepo DrugNotificationRepo { get; }

        void CheckNotifications(List<DrugNotification> notifications, int minutesBeforeShowing);

        List<DrugNotification> CreateNotifications(Patient patient);

        List<DrugNotification> DrugNotifications();

        int GenerateId();

        DrugNotification FindById(int id);
        
    }
}