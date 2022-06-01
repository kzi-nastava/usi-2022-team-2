using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.ReferralRepo
{
    class ReferralRepoFactory
    {
        private ReferralRepo referralRepo;

        public ReferralRepo CreateReferralRepository()
        {
            if (referralRepo == null)
                referralRepo = new ReferralRepo();

            return referralRepo;
        }
    }
}
