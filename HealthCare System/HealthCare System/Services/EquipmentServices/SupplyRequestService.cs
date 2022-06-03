using HealthCare_System.Repository.EquipmentRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomServices;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Services.EquipmentServices
{
    class SupplyRequestService
    {
        SupplyRequestRepo supplyRequestRepo;
        RoomService roomService;

        public SupplyRequestService(SupplyRequestRepo supplyRequestRepo, RoomService roomService)
        {
            this.supplyRequestRepo = supplyRequestRepo;
            this.roomService = roomService;
        }

        public SupplyRequestRepo SupplyRequestRepo { get => supplyRequestRepo; }

        public List<SupplyRequest> SupplyRequests()
        {
            return supplyRequestRepo.SupplyRequests;
        }

        public void TryToExecuteSupplyRequest()
        {
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
