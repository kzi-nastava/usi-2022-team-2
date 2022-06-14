using HealthCare_System.Core.Notifications.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDrugNotificationRepo
    {
        DrugNotification FindById(int id);

        List<DrugNotification> DrugNotifications { get; set; }
        int GenerateId();
    }
}