﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.NotificationRepo;

namespace HealthCare_System.Services.NotificationService
{
    class DrugNotificationService
    {
        DrugNotificationRepo drugNotificationRepo;

        public DrugNotificationService()
        {
            DrugNotificationRepoFactory drugNotificationRepoFactory = new();
            drugNotificationRepo = drugNotificationRepoFactory.CreateDrugNotificationRepository();
        }

        public DrugNotificationRepo DrugNotificationRepo { get => drugNotificationRepo; }
    }
}