using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.ReferralRepo;

namespace HealthCare_System.Services.ReferralService
{
    class ReferralService
    {
        ReferralRepo referralRepo;

        public ReferralService()
        {
            ReferralRepoFactory referralRepoFactory = new();
            referralRepo = referralRepoFactory.CreateReferralRepository();
        }

        public ReferralRepo ReferralRepo { get => referralRepo;}
    }
}
