using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Notifications.Repository;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Notifications
{
    class DaysOffNotificationService : IDaysOffNotificationService
    {
        IDaysOffNotificationRepo daysOffNotificationRepo;

        public DaysOffNotificationService(IDaysOffNotificationRepo daysOffNotificationRepo)
        {
            this.daysOffNotificationRepo = daysOffNotificationRepo;
        }

        public void AddDaysOffNotification(Doctor doctor, string message)
        {
            daysOffNotificationRepo.Add(doctor, message);
        }

        public List<DaysOffNotification> DaysOffNotifications()
        {
            return daysOffNotificationRepo.DaysOffNotifications;
        }
    }
}
