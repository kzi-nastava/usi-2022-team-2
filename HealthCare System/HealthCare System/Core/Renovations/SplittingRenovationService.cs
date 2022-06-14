using System;
using System.Collections.Generic;
using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;

namespace HealthCare_System.Core.Renovations
{
    public class SplittingRenovationService : ISplittingRenovationService
    {
        ISplittingRenovationRepo splittingRenovationRepo;
        IRoomService roomService;
        IEquipmentTransferService equipmentTransferService;
        IEquipmentService equipmentService;

        public SplittingRenovationService(ISplittingRenovationRepo splittingRenovationRepo, IRoomService roomService,
            IEquipmentTransferService equipmentTransferService, IEquipmentService equipmentService)
        {
            this.splittingRenovationRepo = splittingRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public ISplittingRenovationRepo SplittingRenovationRepo { get => splittingRenovationRepo; }

        public List<SplittingRenovation> SplittingRenovations()
        {
            return splittingRenovationRepo.SplittingRenovations;
        }

        public void BookRenovation(SplittingRenovationDto splittingRenovationDto)
        {
            SplittingRenovation splittingRenovation = new SplittingRenovation(splittingRenovationDto);
            splittingRenovationRepo.Add(splittingRenovation);
        }

        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.ACTIVE;
            Serialize();
            equipmentTransferService.MoveEquipmentToStorage(splittingRenovation.Room);
            roomService.Serialize();
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
            if (SplittingRenovations().Count > 0)
            {
                foreach (SplittingRenovation splittingRenovation in SplittingRenovations())
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

        public SplittingRenovation FindById(int id)
        {
            return splittingRenovationRepo.FindById(id);
        }

        public int GenerateId()
        {
            return splittingRenovationRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv")
        {
            splittingRenovationRepo.Serialize();
        }

    }
}
