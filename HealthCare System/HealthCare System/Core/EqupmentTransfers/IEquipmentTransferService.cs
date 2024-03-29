﻿using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.EquipmentTransfers.Repository;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.EquipmentTransfers
{
    public interface IEquipmentTransferService
    {
        IEquipmentTransferRepo EquipmentTransferRepo { get; }

        public IRoomService RoomService { get; set; }

        void Add(TransferDto transferDTO);

        bool CheckWithOthers(Transfer newTransfer);

        void ExecuteTransfer(Transfer transfer);

        void ExecuteTransfer(TransferDto transferDto);

        void MoveEquipmentToStorage(Room room);

        void MoveFromRoom(Room room, Equipment equipmnet, int amount);

        void MoveToRoom(Room room, Equipment equipmnet, int amount);

        List<Transfer> Transfers();

        public Transfer FindById(int id);

        public void Serialize();

        public int GenerateId();

        public void Delete(Transfer transfer);
    }
}