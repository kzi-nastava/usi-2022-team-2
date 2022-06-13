using HealthCare_System.Core.Notifications.Model;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDaysOffNotificationRepo
    {
        string Path { get; set; }

        DaysOffNotification FindById(int id);
        void Serialize();
    }
}