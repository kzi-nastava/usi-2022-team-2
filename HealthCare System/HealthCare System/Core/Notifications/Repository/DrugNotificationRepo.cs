using System.Collections.Generic;
using HealthCare_System.Core.Notifications.Model;

namespace HealthCare_System.Core.Notifications.Repository
{
    public class DrugNotificationRepo
    {
        List<DrugNotification> drugNotifications;

        public DrugNotificationRepo()
        {
            drugNotifications = new();
        }

        internal List<DrugNotification> DrugNotifications { get => drugNotifications; set => drugNotifications = value; }


        public int GenerateId()
        {
            if (drugNotifications.Count == 0)
                return 1;
            return drugNotifications[^1].Id + 1;
        }
        public DrugNotification FindById(int id)
        {
            foreach (DrugNotification drugNofiticaion in drugNotifications)
                if (drugNofiticaion.Id == id)
                    return drugNofiticaion;
            return null;
        }
    }
}
