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
    }
}
