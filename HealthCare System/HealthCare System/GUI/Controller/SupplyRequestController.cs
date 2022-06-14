using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.SupplyRequests;
using HealthCare_System.Core.SupplyRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class SupplyRequestController
    {
        private readonly ISupplyRequestService supplyRequestService;

        public SupplyRequestController(ISupplyRequestService supplyRequestService)
        {
            this.supplyRequestService = supplyRequestService;
        }

        public List<SupplyRequest> SupplyRequests()
        {
            return supplyRequestService.SupplyRequests();
        }

        public void TryToExecuteSupplyRequest()
        {
            supplyRequestService.TryToExecuteSupplyRequest();
        }

        public void AddSupplyRequest(Equipment equipment, int quantity)
        {
            supplyRequestService.AddSupplyRequest(equipment, quantity);
        }

        public SupplyRequest FindById(int id)
        {
            return supplyRequestService.FindById(id);
        }

        public int GenerateId()
        {
            return supplyRequestService.GenerateId();
        }
        public void Serialize()
        {
            supplyRequestService.Serialize();
        }

        public void Delete(SupplyRequest supplyRequest)
        {
            supplyRequestService.Delete(supplyRequest);
        }
    }
}

