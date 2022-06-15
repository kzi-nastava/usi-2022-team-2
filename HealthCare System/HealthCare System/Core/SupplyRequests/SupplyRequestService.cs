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
        ISupplyRequestRepo supplyRequestRepo;
        IRoomService roomService;
        IEquipmentTransferService equipmentTransferService;

        public SupplyRequestService(ISupplyRequestRepo supplyRequestRepo, IRoomService roomService, IEquipmentTransferService equipmentTransferService)
        {
            this.supplyRequestRepo = supplyRequestRepo;
            this.RoomService = roomService;
            this.EquipmentTransferService = equipmentTransferService;
        }

        public ISupplyRequestRepo SupplyRequestRepo { get => supplyRequestRepo; }
        public IRoomService RoomService { get => roomService; set => roomService = value; }
        public IEquipmentTransferService EquipmentTransferService { get => equipmentTransferService; set => equipmentTransferService = value; }

        public List<SupplyRequest> SupplyRequests()
        {
            return supplyRequestRepo.SupplyRequests;
        }

        public void TryToExecuteSupplyRequest()
        {
            Room storage = RoomService.Storage();
            foreach (SupplyRequest supplyRequest in supplyRequestRepo.SupplyRequests)
            {
                if (supplyRequest.Finished == false && DateTime.Now < supplyRequest.RequestCreated.AddDays(1))
                {
                    foreach (Equipment equipment in supplyRequest.OrderDetails.Keys)
                    {
                        EquipmentTransferService.MoveToRoom(storage, equipment, supplyRequest.OrderDetails[equipment]);
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

        public SupplyRequest FindById(int id)
        {
            return supplyRequestRepo.FindById(id);
        }

        public int GenerateId()
        {
            return supplyRequestRepo.GenerateId();
        }
        public void Serialize()
        {
            supplyRequestRepo.Serialize();
        }

        public void Delete(SupplyRequest supplyRequest)
        {
            supplyRequestRepo.Delete(supplyRequest);
        }
    }
}
