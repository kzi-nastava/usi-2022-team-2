using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.SupplyRequests.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.SupplyRequests
{
    public interface ISupplyRequestService
    {
        void AddSupplyRequest(Equipment equipment, int quantity);
        List<SupplyRequest> SupplyRequests();
        void TryToExecuteSupplyRequest();
    }
}