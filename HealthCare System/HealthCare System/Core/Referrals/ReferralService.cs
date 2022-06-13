using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using HealthCare_System.Repository.ReferralRepo;

namespace HealthCare_System.Core.Referrals
{
    public class ReferralService
    {
        ReferralRepo referralRepo;

        public ReferralService(ReferralRepo referralRepo)
        {
            this.referralRepo = referralRepo;
        }

        public ReferralRepo ReferralRepo { get => referralRepo;}

        public void Add(ReferralDto referralDto)
        {
            Referral referral = new(referralDto);
            referralRepo.Add(referral);
        }
    }
}
