using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DrugNotificationController
    {
        List<DrugNotification> drugNotifications;

        public DrugNotificationController()
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
