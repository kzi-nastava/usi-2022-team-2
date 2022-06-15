using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.Rooms.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.EquipmentTransfers
{
    class EquipmentTransferController
    {
        private readonly IEquipmentTransferService equipmentTransferService;

        public EquipmentTransferController(IEquipmentTransferService equipmentTransferService)
        {
            this.equipmentTransferService = equipmentTransferService;
        }

        public void Add(TransferDto transferDTO)
        {
            equipmentTransferService.Add(transferDTO);
        }

        public bool CheckWithOthers(Transfer newTransfer)
        {
            return equipmentTransferService.CheckWithOthers(newTransfer);
        }

        public void ExecuteTransfer(Transfer transfer)
        {
            equipmentTransferService.ExecuteTransfer(transfer);
        }

        public void ExecuteTransfer(TransferDto transferDto)
        {
            equipmentTransferService.ExecuteTransfer(transferDto);
        }

        public void MoveEquipmentToStorage(Room room)
        {
            equipmentTransferService.MoveEquipmentToStorage(room);
        }

        public void MoveFromRoom(Room room, Equipment equipmnet, int amount)
        {
            equipmentTransferService.MoveFromRoom(room, equipmnet, amount);
        }

        public void MoveToRoom(Room room, Equipment equipmnet, int amount)
        {
            equipmentTransferService.MoveToRoom(room, equipmnet, amount);
        }

        public List<Transfer> Transfers()
        {
            return equipmentTransferService.Transfers();
        }

        public Transfer FindById(int id)
        {
            return equipmentTransferService.FindById(id);
        }

        public void Serialize()
        {
            equipmentTransferService.Serialize();
        }

        public int GenerateId()
        {
            return equipmentTransferService.GenerateId();
        }

        public void Delete(Transfer transfer)
        {
            equipmentTransferService.Delete(transfer);
        }
    }
}
