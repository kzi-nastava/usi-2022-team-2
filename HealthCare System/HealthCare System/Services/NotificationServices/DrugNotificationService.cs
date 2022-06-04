using System.Collections.Generic;
using HealthCare_System.Repository.NotificationRepo;
using HealthCare_System.Model;

namespace HealthCare_System.Services.NotificationServices
{
    public class DrugNotificationService
    {
        DrugNotificationRepo drugNotificationRepo;

        public DrugNotificationService(DrugNotificationRepo drugNotificationRepo)
        {
            this.drugNotificationRepo = drugNotificationRepo;
        }

        public List<DrugNotification> DrugNotifications()
        {
            return drugNotificationRepo.DrugNotifications;
        }

        public DrugNotificationRepo DrugNotificationRepo { get => drugNotificationRepo; }
    }
}
