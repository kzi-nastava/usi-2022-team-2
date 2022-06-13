using HealthCare_System.Core.Referrals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Referrals.Repository
{
    public interface IReferralRepo
    {

        void Load();

        public Referral FindById(int id);

        public int GenerateId();

        public void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv");

        public void Add(Referral referral);
    }
}

