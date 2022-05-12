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
            path = "../../../data/entities/Referrals.json";
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

        public int GenerateId()
        {
            return referrals[^1].Id + 1;
        }

        public void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv")
        {
            string referralsJson = JsonSerializer.Serialize(referrals, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, referralsJson);

            string csv = "";
            foreach (Referral referral in referrals)
            {
                string doctorJmbg;
                if (referral.Doctor is null) doctorJmbg = "-1";
                else doctorJmbg = referral.Doctor.Jmbg;

                csv += referral.Id + ";" + doctorJmbg + ";" + referral.MedicalRecord.Id + "\n";
            }

            File.WriteAllText(linkPath, csv);
        }

        public void Add(Referral referral)
        {
            referrals.Add(referral);
            Serialize();
        }
    }
}
