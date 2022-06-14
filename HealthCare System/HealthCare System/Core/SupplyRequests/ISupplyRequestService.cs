using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.SupplyRequests.Model;
using HealthCare_System.Core.SupplyRequests.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.SupplyRequests
{
    public interface ISupplyRequestService
    {
        ISupplyRequestRepo SupplyRequestRepo { get ; }

        void AddSupplyRequest(Equipment equipment, int quantity);
        List<SupplyRequest> SupplyRequests();
        void TryToExecuteSupplyRequest();

        public SupplyRequest FindById(int id);

        public int GenerateId();

        public void Serialize();

        public void Delete(SupplyRequest supplyRequest);
        
    }
}