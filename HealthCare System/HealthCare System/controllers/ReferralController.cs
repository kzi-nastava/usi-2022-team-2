using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class ReferralController
    {
        List<Referral> referrals;

        public ReferralController()
        {
            Load();
        }

        internal List<Referral> Referrals { get => referrals; set => referrals = value; }

        void Load()
        {
            referrals = JsonSerializer.Deserialize<List<Referral>>(File.ReadAllText("data/entities/Referrals.json"));
        }

        public Referral FindById(int id)
        {
            foreach (Referral referral in referrals)
                if (referral.Id == id)
                    return referral;
            return null;
        }
    }
}
