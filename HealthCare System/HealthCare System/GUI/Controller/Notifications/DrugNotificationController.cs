using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Notifications
{
    class DrugNotificationController
    {
        private readonly IDrugNotificationService drugNotificationService;

        public DrugNotificationController(IDrugNotificationService drugNotificationService)
        {
            this.drugNotificationService = drugNotificationService;
        }

        public List<DrugNotification> DrugNotifications()
        {
            return drugNotificationService.DrugNotifications();
        }

        public void CheckNotifications(List<DrugNotification> notifications, int minutesBeforeShowing)
        {
            drugNotificationService.CheckNotifications(notifications, minutesBeforeShowing);
        }

        public List<DrugNotification> CreateNotifications(Patient patient)
        {
            return drugNotificationService.CreateNotifications(patient);
        }

        public int GenerateId()
        {
            return drugNotificationService.GenerateId();
        }

        public DrugNotification FindById(int id)
        {
            return drugNotificationService.FindById(id);
        }
    }
}
