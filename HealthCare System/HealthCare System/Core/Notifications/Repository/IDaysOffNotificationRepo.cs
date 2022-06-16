using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDaysOffNotificationRepo
    {
        string Path { get; set; }

        List<DaysOffNotification> DaysOffNotifications { get; set; }

        public void Add(Doctor doctor, string message);
        DaysOffNotification FindById(int id);
        void Serialize();

        public int GenerateId();
    }
}