using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public class MergingRenovationService : IMergingRenovationService
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

        public MergingRenovationRepo MergingRenovationRepo { get => mergingRenovationRepo; }

        public List<MergingRenovation> MergingRenovations()
        {
            return mergingRenovationRepo.MergingRenovations;
        }

        public void BookRenovation(MergingRenovationDto mergingRenovationDto)
        {
            MergingRenovation mergingRenovation = new MergingRenovation(mergingRenovationDto);
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
            roomService.Create(mergingRenovation.NewRoomName, mergingRenovation.NewRoomType, equipmentAmount);
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
