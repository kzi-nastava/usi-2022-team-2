using HealthCare_System.Repository.EquipmentRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomService;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Services.EquipmentService
{
    class SupplyRequestService
    {
        SupplyRequestRepo supplyRequestRepo;
        RoomService.RoomService roomService;

        public SupplyRequestService()
        {
            SupplyRequestRepoFactory supplyRequestRepoFactory = new();
            supplyRequestRepo = supplyRequestRepoFactory.CreateSupplyRequestRepository();
        }

        public SupplyRequestRepo SupplyRequestRepo { get => supplyRequestRepo; }

        public List<SupplyRequest> SupplyRequests()
        {
            return supplyRequestRepo.SupplyRequests;
        }

        public void TryToExecuteSupplyRequest()
        {
            roomService = new();
            Room storage = roomService.Storage();
            foreach (SupplyRequest supplyRequest in supplyRequestRepo.SupplyRequests)
            {
                if (supplyRequest.Finished == false && DateTime.Now < supplyRequest.RequestCreated.AddDays(1))
                {
                    foreach (Equipment equipment in supplyRequest.OrderDetails.Keys)
                    {
                        // staviti metodu MoveToRoom() i serijalizovati sobe?
                        storage.EquipmentAmount[equipment] += supplyRequest.OrderDetails[equipment];
                    }
                }
            }
        }

        public void AddSupplyRequest(Equipment equipment, int quantity)
        {
            SupplyRequest supplyRequest = new SupplyRequest(supplyRequestRepo.GenerateId(), equipment, quantity);
            supplyRequestRepo.Add(supplyRequest);
        }
    }
}
