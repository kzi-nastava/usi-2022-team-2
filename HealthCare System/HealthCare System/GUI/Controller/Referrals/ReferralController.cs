using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Referrals.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Referrals
{
    class ReferralController
    {
        private readonly IReferralService referralService;

        public ReferralController(IReferralService referralService)
        {
            this.referralService = referralService;
        }

        public List<Referral> Referrals()
        {
            return referralService.Referrals();
        }

        public void Add(ReferralDto referralDto)
        {
            referralService.Add(referralDto);
        }

        public Referral FindById(int id)
        {
            return referralService.FindById(id);
        }

        public int GenerateId()
        {
            return referralService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv")
        {
            referralService.Serialize();
        }
    }
}
