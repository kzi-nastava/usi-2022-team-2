using HealthCare_System.Core.Renovations.Model;

namespace HealthCare_System.Core.Renovations.Repository
{
    public interface ISimpleRenovationRepo
    {
        void Add(SimpleRenovation simpleRenovation);
        void Delete(SimpleRenovation simpleRenovation);
        SimpleRenovation FindById(int id);
        int GenerateId();
        void Load();
        void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv");
    }
}