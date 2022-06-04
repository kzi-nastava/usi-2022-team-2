using System;
using System.Collections.Generic;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.EquipmentServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.Services.RenovationServices
{
    public class SplittingRenovationService
    {
        SplittingRenovationRepo splittingRenovationRepo;
        RoomService roomService;
        EquipmentTransferService equipmentTransferService;
        EquipmentService equipmentService;

        public SplittingRenovationService(SplittingRenovationRepo splittingRenovationRepo, RoomService roomService, 
            EquipmentTransferService equipmentTransferService, EquipmentService equipmentService)
        {
            this.splittingRenovationRepo = splittingRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public SplittingRenovationRepo SplittingRenovationRepo { get => splittingRenovationRepo;}

        public List<SplittingRenovation> SplittingRenovations()
        {
            return splittingRenovationRepo.SplittingRenovations;
        }

        public void BookRenovation(SplittingRenovationDTO splittingRenovationDTO)
        {
            SplittingRenovation splittingRenovation = new SplittingRenovation(splittingRenovationDTO);
            splittingRenovationRepo.Add(splittingRenovation);
        }

        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.ACTIVE;
            splittingRenovationRepo.Serialize();
            equipmentTransferService.MoveEquipmentToStorage(splittingRenovation.Room);
            roomService.RoomRepo.Serialize();
        }

        public void FinishSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.FINISHED;
            roomService.RemoveRoom(splittingRenovation.Room);
            Dictionary<Equipment, int> firstRoomEquipmentAmount = equipmentService.InitalizeEquipment();
            roomService.Create(splittingRenovation.FirstNewRoomName, splittingRenovation.FirstNewRoomType,
                firstRoomEquipmentAmount);
            Dictionary<Equipment, int> secondRoomEquipmentAmount = equipmentService.InitalizeEquipment();
            roomService.Create(splittingRenovation.SecondNewRoomName, splittingRenovation.SecondNewRoomType,
                secondRoomEquipmentAmount);
            splittingRenovationRepo.Delete(splittingRenovation);
        }

        public void TryToExecuteSplittingRenovations()
        {
            if (splittingRenovationRepo.SplittingRenovations.Count > 0)
            {
                foreach (SplittingRenovation splittingRenovation in splittingRenovationRepo.SplittingRenovations)
                {
                    if (DateTime.Now >= splittingRenovation.EndingDate)
                    {
                        FinishSplittingRenovation(splittingRenovation);
                        return;
                    }

                    if (DateTime.Now >= splittingRenovation.BeginningDate &&
                        splittingRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartSplittingRenovation(splittingRenovation);
                        return;
                    }
                }
            }

        }
    }
}
