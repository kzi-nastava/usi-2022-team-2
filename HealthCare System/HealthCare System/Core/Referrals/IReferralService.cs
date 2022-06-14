using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Referrals.Repository;

namespace HealthCare_System.Core.Referrals
{
    public interface IReferralService
    {
        IReferralRepo ReferralRepo { get; }

        void Add(ReferralDto referralDto);
    }
}