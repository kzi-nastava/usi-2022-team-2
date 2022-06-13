using HealthCare_System.Core.Notifications.Model;

namespace HealthCare_System.Core.Notifications.Repository
{
    public interface IDrugNotificationRepo
    {
        DrugNotification FindById(int id);
        int GenerateId();
    }
}