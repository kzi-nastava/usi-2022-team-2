using HealthCare_System.Repository.EquipmentRepo;

namespace HealthCare_System.Services.EquipmentService
{
    class SupplyRequestService
    {
        SupplyRequestRepo supplyRequestRepo;

        public SupplyRequestService()
        {
            SupplyRequestRepoFactory supplyRequestRepoFactory = new();
            supplyRequestRepo = supplyRequestRepoFactory.CreateSupplyRequestRepository();
        }

        public SupplyRequestRepo SupplyRequestRepo { get => supplyRequestRepo; }

        public void TryToExecuteSupplyRequest()
        {
            Room storage = roomController.GetStorage();
            foreach (SupplyRequest supplyRequest in supplyRequestController.SupplyRequests)
            {
                if (supplyRequest.Finished == false && DateTime.Now < supplyRequest.RequestCreated.AddDays(1))
                {
                    foreach (Equipment equipment in supplyRequest.OrderDetails.Keys)
                    {
                        storage.EquipmentAmount[equipment] += supplyRequest.OrderDetails[equipment];
                    }
                }
            }
        }

        public void AddSupplyRequest(Equipment equipment, int quantity)
        {
            SupplyRequest supplyRequest = new SupplyRequest(supplyRequestController.GenerateId(), equipment, quantity);
            supplyRequestController.SupplyRequests.Add(supplyRequest);
            supplyRequestController.Serialize();
        }
    }
}
