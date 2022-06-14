using HealthCare_System.Core.SupplyRequests.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.SupplyRequests.Repository
{
    public interface ISupplyRequestRepo
    {
        List<SupplyRequest> SupplyRequests { get ; set ; }
        void Add(SupplyRequest supplyRequest);
        void Delete(SupplyRequest supplyRequest);
        SupplyRequest FindById(int id);
        int GenerateId();
        void Load();
        void Serialize();
    }
}