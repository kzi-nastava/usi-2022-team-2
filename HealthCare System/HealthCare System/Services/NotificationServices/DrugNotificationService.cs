using System.Collections.Generic;
using HealthCare_System.Repository.NotificationRepo;
using HealthCare_System.Model;
using System;
using System.Windows;

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
    }
}
