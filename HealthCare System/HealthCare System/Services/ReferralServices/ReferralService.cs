using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.ReferralRepo;

namespace HealthCare_System.Services.ReferralServices
{
    class ReferralService
    {
        ReferralRepo referralRepo;

        public ReferralService(ReferralRepo referralRepo)
        {
            this.referralRepo = referralRepo;
        }

        public ReferralRepo ReferralRepo { get => referralRepo;}
    }
}
