using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Referrals.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Referrals
{
    public class ReferralService : IReferralService
    {
        IReferralRepo referralRepo;

        public ReferralService(IReferralRepo referralRepo)
        {
            this.referralRepo = referralRepo;
        }

        public IReferralRepo ReferralRepo { get => referralRepo; }

        public List<Referral> Referrals()
        {
            return referralRepo.Referrals;
        }

        public void Add(ReferralDto referralDto)
        {
            Referral referral = new(referralDto);
            referralRepo.Add(referral);
        }

        public Referral FindById(int id)
        {
            return referralRepo.FindById(id);
        }

        public int GenerateId()
        {
            return referralRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv")
        {
            referralRepo.Serialize();
        }
    }
}
