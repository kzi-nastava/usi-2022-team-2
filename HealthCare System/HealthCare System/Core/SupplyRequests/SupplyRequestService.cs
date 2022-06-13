using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.SupplyRequests.Model;
using HealthCare_System.Core.SupplyRequests.Repository;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.SupplyRequests
{
    public class SupplyRequestService : ISupplyRequestService
    {
        SupplyRequestRepo supplyRequestRepo;
        RoomService roomService;
        EquipmentTransferService equipmentTransferService;

        public SupplyRequestService(SupplyRequestRepo supplyRequestRepo, RoomService roomService, EquipmentTransferService equipmentTransferService)
        {
            this.supplyRequestRepo = supplyRequestRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
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
                        equipmentTransferService.MoveToRoom(storage, equipment, supplyRequest.OrderDetails[equipment]);
                    }
                    supplyRequest.Finished = true;
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
