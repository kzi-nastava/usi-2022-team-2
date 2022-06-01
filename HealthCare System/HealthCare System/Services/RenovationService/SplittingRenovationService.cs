using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.RenovationRepo;

namespace HealthCare_System.Services.RenovationService
{
    class SplittingRenovationService
    {
        SplittingRenovationRepo splittingRenovationRepo;

        public SplittingRenovationService()
        {
            SplittingRenovationRepoFactory splittingRenovationRepoFactory = new();
            splittingRenovationRepo = splittingRenovationRepoFactory.CreateSplittingRenovationRepository();
        }

        public SplittingRenovationRepo SplittingRenovationRepo { get => splittingRenovationRepo;}

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string firstNewRoomName, TypeOfRoom firstNewRoomType, string secondNewRoomName,
            TypeOfRoom secondNewRoomType)
        {
            SplittingRenovation splittingRenovation = new SplittingRenovation(GenerateId(), start, end,
                RenovationStatus.BOOKED, room, firstNewRoomName, firstNewRoomType,
                secondNewRoomName, secondNewRoomType);
            splittingRenovations.Add(splittingRenovation);
            Serialize();
        }

        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.ACTIVE;
            splittingRenovationController.Serialize();
            roomController.MoveEquipmentToStorage(splittingRenovation.Room);
            roomController.Serialize();
        }

        public void FinishSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.FINISHED;
            RemoveRoom(splittingRenovation.Room);
            Dictionary<Equipment, int> firstRoomEquipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(splittingRenovation.FirstNewRoomName, splittingRenovation.FirstNewRoomType,
                firstRoomEquipmentAmount);
            Dictionary<Equipment, int> secondRoomEquipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(splittingRenovation.SecondNewRoomName, splittingRenovation.SecondNewRoomType,
                secondRoomEquipmentAmount);
            splittingRenovationController.SplittingRenovations.Remove(splittingRenovation);
            splittingRenovationController.Serialize();
        }

        public void TryToExecuteSplittingRenovations()
        {
            if (splittingRenovationController.SplittingRenovations.Count > 0)
            {
                foreach (SplittingRenovation splittingRenovation in splittingRenovationController.SplittingRenovations)
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
