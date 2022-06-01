using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.NotificationRepo
{
    class DaysOffNotificationRepoFactory
    {
        private DaysOffNotificationRepo daysOffNotificationRepo;

        public DaysOffNotificationRepo CreateDaysOffNotificationRepository()
        {
            if (daysOffNotificationRepo == null)
                daysOffNotificationRepo = new DaysOffNotificationRepo();

            return daysOffNotificationRepo;
        }
    }
}
