using System;
using System.Collections.Generic;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.EquipmentTransfers.Repository;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;

namespace HealthCare_System.Core.EquipmentTransfers
{
    public class EquipmentTransferService : IEquipmentTransferService
    {
        IEquipmentTransferRepo equipmentTransferRepo;
        IRoomService roomService;

        public EquipmentTransferService(IEquipmentTransferRepo equipmentTransferRepo, IRoomService roomService)
        {
            this.equipmentTransferRepo = equipmentTransferRepo;
            this.roomService = roomService;
        }

        public IEquipmentTransferRepo EquipmentTransferRepo { get => equipmentTransferRepo; }

        public IRoomService RoomService { get => roomService; set => roomService = value; }

        public List<Transfer> Transfers()
        {
            return equipmentTransferRepo.Transfers;
        }

        public void MoveToRoom(Room room, Equipment equipmnet, int amount)
        {
            room.EquipmentAmount[equipmnet] += amount;
        }

        public void MoveFromRoom(Room room, Equipment equipmnet, int amount)
        {
            if (room.EquipmentAmount[equipmnet] < amount)
                throw new Exception("Amount to be moved is larger then current amount in a room");
            room.EquipmentAmount[equipmnet] -= amount;
        }

        public void MoveEquipmentToStorage(Room room)
        {
            foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in room.EquipmentAmount)
            {
                MoveFromRoom(room, equipmentAmountEntry.Key, equipmentAmountEntry.Value);
                MoveToRoom(roomService.Storage(), equipmentAmountEntry.Key, equipmentAmountEntry.Value);
            }
        }

        public void Add(TransferDto transferDTO)
        {
            Transfer transfer = new Transfer(transferDTO);
            if (!CheckWithOthers(transfer))
                throw new Exception("Entered amount to be moved is larger than amount availabel " +
                    "in the room after all the transfers are finished.");
            equipmentTransferRepo.Add(transfer);
        }

        public bool CheckWithOthers(Transfer newTransfer)
        {
            bool valid = true;
            int amountOfEquipmentStashed = 0;
            if (equipmentTransferRepo.Transfers.Count > 0)
            {
                foreach (Transfer stashedTransfer in equipmentTransferRepo.Transfers)
                {
                    if (newTransfer.FromRoom == stashedTransfer.FromRoom &&
                        newTransfer.Equipment == stashedTransfer.Equipment)
                        amountOfEquipmentStashed += stashedTransfer.Amount;
                }

                int amountInFroomRoom = newTransfer.FromRoom.EquipmentAmount[newTransfer.Equipment];
                int amountLeft = amountInFroomRoom - amountOfEquipmentStashed;
                if (amountLeft < newTransfer.Amount)
                    valid = false;
            }
            return valid;
        }

        public void ExecuteTransfer(TransferDto transferDto)
        {
            MoveFromRoom(transferDto.FromRoom, transferDto.Equipment, transferDto.Amount);
            MoveToRoom(transferDto.ToRoom, transferDto.Equipment, transferDto.Amount);
            roomService.Serialize();
        }

        public void ExecuteTransfer(Transfer transfer)
        {
            MoveFromRoom(transfer.FromRoom, transfer.Equipment, transfer.Amount);
            MoveToRoom(transfer.ToRoom, transfer.Equipment, transfer.Amount);
            equipmentTransferRepo.Delete(transfer);
            roomService.Serialize();
        }

        public Transfer FindById(int id)
        {
            return equipmentTransferRepo.FindById(id);
        }

        public void Serialize()
        {
            equipmentTransferRepo.Serialize();
        }

        public int GenerateId()
        {
            return equipmentTransferRepo.GenerateId();
        }

        public void Delete(Transfer transfer)
        {
            equipmentTransferRepo.Delete(transfer);
        }
    }
}
