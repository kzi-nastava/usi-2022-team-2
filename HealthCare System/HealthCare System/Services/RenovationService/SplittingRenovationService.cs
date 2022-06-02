using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomService;
using HealthCare_System.Services.EquipmentService;

namespace HealthCare_System.Services.RenovationService
{
    class SplittingRenovationService
    {
        SplittingRenovationRepo splittingRenovationRepo;
        RoomService.RoomService roomService;
        EquipmentTransferService equipmentTransferService;
        EquipmentService.EquipmentService equipmentService;

        public SplittingRenovationService()
        {
            SplittingRenovationRepoFactory splittingRenovationRepoFactory = new();
            splittingRenovationRepo = splittingRenovationRepoFactory.CreateSplittingRenovationRepository();
        }

        public SplittingRenovationRepo SplittingRenovationRepo { get => splittingRenovationRepo;}

        public List<SplittingRenovation> SplittingRenovations()
        {
            return splittingRenovationRepo.SplittingRenovations;
        }

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string firstNewRoomName, TypeOfRoom firstNewRoomType, string secondNewRoomName,
            TypeOfRoom secondNewRoomType)
        {
            SplittingRenovation splittingRenovation = new SplittingRenovation(splittingRenovationRepo.GenerateId(), start, end,
                RenovationStatus.BOOKED, room, firstNewRoomName, firstNewRoomType,
                secondNewRoomName, secondNewRoomType);
            splittingRenovationRepo.Add(splittingRenovation);
        }

        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            equipmentTransferService = new();
            roomService = new();
            splittingRenovation.Status = RenovationStatus.ACTIVE;
            splittingRenovationRepo.Serialize();
            equipmentTransferService.MoveEquipmentToStorage(splittingRenovation.Room);
            roomService.RoomRepo.Serialize();
        }

        public void FinishSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            roomService = new();
            equipmentService = new();
            splittingRenovation.Status = RenovationStatus.FINISHED;
            roomService.RemoveRoom(splittingRenovation.Room);
            Dictionary<Equipment, int> firstRoomEquipmentAmount = equipmentService.InitalizeEquipment();
            roomService.CreateNewRoom(splittingRenovation.FirstNewRoomName, splittingRenovation.FirstNewRoomType,
                firstRoomEquipmentAmount);
            Dictionary<Equipment, int> secondRoomEquipmentAmount = equipmentService.InitalizeEquipment();
            roomService.CreateNewRoom(splittingRenovation.SecondNewRoomName, splittingRenovation.SecondNewRoomType,
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
