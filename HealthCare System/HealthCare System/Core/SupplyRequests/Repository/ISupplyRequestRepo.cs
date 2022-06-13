using HealthCare_System.Core.SupplyRequests.Model;

namespace HealthCare_System.Core.SupplyRequests.Repository
{
    public interface ISupplyRequestRepo
    {
        void Add(SupplyRequest supplyRequest);
        void Delete(SupplyRequest supplyRequest);
        SupplyRequest FindById(int id);
        int GenerateId();
        void Load();
        void Serialize();
    }
}