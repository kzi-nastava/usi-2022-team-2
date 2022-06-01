using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.EquipmentRepo;

namespace HealthCare_System.Services.EquipmentService
{
    class EquipmentTransferService
    {
        EquipmentTransferRepo equipmentTransferRepo;

        public EquipmentTransferService()
        {
            EquipmentTransferRepoFactory equipmentTransferRepoFactory = new();
            equipmentTransferRepo = equipmentTransferRepoFactory.CreateEquipmentTransferRepository();
        }

        public EquipmentTransferRepo EquipmentTransferRepo { get => equipmentTransferRepo; }

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
                MoveToRoom(FindById(1003), equipmentAmountEntry.Key, equipmentAmountEntry.Value);
            }
        }
        public void Add(DateTime momentOfTransfer, Room fromRoom, Room toRoom,
            Equipment equipment, int amount)
        {
            int id = GenerateId();
            Transfer transfer = new Transfer(id, momentOfTransfer, fromRoom, toRoom, equipment, amount);
            if (!CheckWithOthers(transfer))
                throw new Exception("Entered amount to be moved is larger than amount availabel " +
                    "in the room after all the transfers are finished.");
            transfers.Add(transfer);
            Serialize();
        }

        public bool CheckWithOthers(Transfer newTransfer)
        {
            bool valid = true;
            int amountOfEquipmentStashed = 0;
            if (transfers.Count > 0)
            {
                foreach (Transfer stashedTransfer in transfers)
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

        public void ExecuteTransfer(Transfer transfer)
        {
            roomController.MoveFromRoom(transfer.FromRoom, transfer.Equipment, transfer.Amount);
            roomController.MoveToRoom(transfer.ToRoom, transfer.Equipment, transfer.Amount);
            roomController.Serialize();
            transferController.Transfers.Remove(transfer);
            transferController.Serialize();
        }
    }
}
