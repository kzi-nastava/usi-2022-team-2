using HealthCare_System.Core.Referrals.Model;

namespace HealthCare_System.Core.Referrals.Repository
{
    public interface IReferralRepo
    {
        string Path { get; set; }

        void Add(Referral referral);
        Referral FindById(int id);
        int GenerateId();
        void Load();
        void Serialize(string linkPath = "../../../data/links/ReferralLinker.csv");
    }
}