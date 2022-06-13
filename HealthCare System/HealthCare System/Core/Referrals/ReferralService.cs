using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Referrals.Repository;

namespace HealthCare_System.Core.Referrals
{
    public class ReferralService : IReferralService
    {
        ReferralRepo referralRepo;

        public ReferralService(ReferralRepo referralRepo)
        {
            this.referralRepo = referralRepo;
        }

        public ReferralRepo ReferralRepo { get => referralRepo; }

        public void Add(ReferralDto referralDto)
        {
            Referral referral = new(referralDto);
            referralRepo.Add(referral);
        }
    }
}
