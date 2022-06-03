using System;
using System.Collections.Generic;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.EquipmentServices;

namespace HealthCare_System.Services.RenovationServices
{
    class MergingRenovationService
    {
        MergingRenovationRepo mergingRenovationRepo;
        RoomService roomService;
        EquipmentTransferService equipmentTransferService;
        EquipmentService equipmentService;

        public MergingRenovationService(MergingRenovationRepo mergingRenovationRepo, RoomService roomService, 
            EquipmentTransferService equipmentTransferService, EquipmentService equipmentService)
        {
            this.mergingRenovationRepo = mergingRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public MergingRenovationRepo MergingRenovationRepo { get => mergingRenovationRepo;}

        public List<MergingRenovation> MergingRenovations()
        {
            return mergingRenovationRepo.MergingRenovations;
        }

        public void BookRenovation(DateTime start, DateTime end, Room firstRoom,
            Room secondRoom, string newRoomName, TypeOfRoom newRoomType)
        {
            List<Room> rooms = new List<Room> { firstRoom, secondRoom };
            MergingRenovation mergingRenovation = new MergingRenovation(mergingRenovationRepo.GenerateId(), start, end, rooms,
                RenovationStatus.BOOKED, newRoomName, newRoomType);
            mergingRenovationRepo.Add(mergingRenovation);
        }

        public void StartMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            mergingRenovationRepo.Serialize();
            foreach (Room room in mergingRenovation.Rooms)
            {
                equipmentTransferService.MoveEquipmentToStorage(room);
            }
            roomService.RoomRepo.Serialize();
        }

        public void FinishMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            foreach (Room room in mergingRenovation.Rooms)
            {
                roomService.RemoveRoom(room);
            }
            Dictionary<Equipment, int> equipmentAmount = equipmentService.InitalizeEquipment();
            roomService.CreateNewRoom(mergingRenovation.NewRoomName, mergingRenovation.NewRoomType, equipmentAmount);
            mergingRenovationRepo.Delete(mergingRenovation);
        }

        public void TryToExecuteMergingRenovations()
        {
            if (mergingRenovationRepo.MergingRenovations.Count > 0)
            {
                foreach (MergingRenovation mergingRenovation in mergingRenovationRepo.MergingRenovations)
                {
                    if (DateTime.Now >= mergingRenovation.EndingDate)
                    {
                        FinishMergingRenovation(mergingRenovation);
                        return;
                    }

                    if (DateTime.Now >= mergingRenovation.BeginningDate &&
                        mergingRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartMergingRenovation(mergingRenovation);
                        return;
                    }
                }
            }

        }
    }
}
