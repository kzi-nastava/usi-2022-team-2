using System.Collections.Generic;
using System;
using System.Windows;
using HealthCare_System.Core.Notifications.Repository;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Prescriptions.Model;

namespace HealthCare_System.Core.Notifications
{
    public class DrugNotificationService : IDrugNotificationService
    {
        IDrugNotificationRepo drugNotificationRepo;

        public DrugNotificationService(IDrugNotificationRepo drugNotificationRepo)
        {
            this.drugNotificationRepo = drugNotificationRepo;
        }

        public IDrugNotificationRepo DrugNotificationRepo { get => drugNotificationRepo; }

        public List<DrugNotification> DrugNotifications()
        {
            return drugNotificationRepo.DrugNotifications;
        }

        public void CheckNotifications(List<DrugNotification> notifications, int minutesBeforeShowing)
        {
            foreach (DrugNotification notification in notifications)
            {
                if (notification.IsTimeToShow(minutesBeforeShowing) && !notification.Seen)
                {
                    notification.Seen = true;
                    MessageBox.Show(notification.Message);
                }
            }
        }

        public List<DrugNotification> CreateNotifications(Patient patient)
        {
            List<DrugNotification> notifications = new();
            foreach (Prescription prescription in patient.MedicalRecord.Prescriptions)
            {
                if ((prescription.Start <= DateTime.Now && prescription.End >= DateTime.Now) || prescription.Start >= DateTime.Now)
                {
                    DateTime time = prescription.Start;
                    while (time < prescription.End)
                    {
                        string message = "It's time to drink " + prescription.Drug.Name;
                        notifications.Add(new DrugNotification(-1, message, patient, prescription.Drug, time));
                        time = time.AddHours(24 / prescription.Frequency);
                    }
                }
            }
            return notifications;
        }

        public int GenerateId()
        {
            return drugNotificationRepo.GenerateId();
        }

        public DrugNotification FindById(int id)
        {
            return drugNotificationRepo.FindById(id);
        }
    }
}
