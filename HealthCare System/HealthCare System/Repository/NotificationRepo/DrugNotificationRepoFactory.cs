using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.NotificationRepo
{
    class DrugNotificationRepoFactory
    {
        private DrugNotificationRepo drugNotificationRepo;

        public DrugNotificationRepo CreateDrugNotificationRepository()
        {
            if (drugNotificationRepo == null)
                drugNotificationRepo = new DrugNotificationRepo();

            return drugNotificationRepo;
        }
    }
}
