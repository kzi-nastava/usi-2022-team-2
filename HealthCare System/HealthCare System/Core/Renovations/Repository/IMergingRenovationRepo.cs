using HealthCare_System.Core.Renovations.Model;

namespace HealthCare_System.Core.Renovations.Repository
{
    public interface IMergingRenovationRepo
    {
        void Add(MergingRenovation mergingRenovation);
        void Delete(MergingRenovation mergingRenovation);
        MergingRenovation FindById(int id);
        int GenerateId();
        void Load();
        void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv");
    }
}