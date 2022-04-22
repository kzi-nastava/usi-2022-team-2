using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class ReferralController
    {
        List<Referral> referrals;
        string path;

        public ReferralController()
        {
            path = "data/entities/Referrals.json";
            Load();
        }

        public ReferralController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Referral> Referrals { get => referrals; set => referrals = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            referrals = JsonSerializer.Deserialize<List<Referral>>(File.ReadAllText(path));
        }

        public Referral FindById(int id)
        {
            foreach (Referral referral in referrals)
                if (referral.Id == id)
                    return referral;
            return null;
        }

        public void Serialize()
        {
            string referralsJson = JsonSerializer.Serialize(referrals, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, referralsJson);
        }
    }
}
