using System;
using System.Collections.Generic;
using HealthCare_System.Repository.EquipmentRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.Services.EquipmentServices
{
    public class EquipmentTransferService
    {
        EquipmentTransferRepo equipmentTransferRepo;
        RoomService roomService;

        public EquipmentTransferService(EquipmentTransferRepo equipmentTransferRepo, RoomService roomService)
        {
            this.equipmentTransferRepo = equipmentTransferRepo;
            this.roomService = roomService;
        }

        public EquipmentTransferRepo EquipmentTransferRepo { get => equipmentTransferRepo; }

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

        public void Add(TransferDTO transferDTO)
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
            roomService.RoomRepo.Serialize();
        }
    }
}
