using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Referrals.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Referrals
{
    public interface IReferralService
    {
        IReferralRepo ReferralRepo { get; }

        public List<Referral> Referrals();

        void Add(ReferralDto referralDto);

        Referral FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv");
        
    }
}