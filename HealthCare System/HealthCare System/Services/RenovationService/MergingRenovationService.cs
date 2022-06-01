using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.RenovationRepo;

namespace HealthCare_System.Services.RenovationService
{
    class MergingRenovationService
    {
        MergingRenovationRepo mergingRenovationRepo;

        public MergingRenovationService()
        {
            MergingRenovationRepoFactory mergingRenovationRepoFactory = new();
            mergingRenovationRepo = mergingRenovationRepoFactory.CreateMergingRenovationRepository();
        }

        public MergingRenovationRepo MergingRenovationRepo { get => mergingRenovationRepo;}

        public void BookRenovation(DateTime start, DateTime end, Room firstRoom,
            Room secondRoom, string newRoomName, TypeOfRoom newRoomType)
        {
            List<Room> rooms = new List<Room> { firstRoom, secondRoom };
            MergingRenovation mergingRenovation = new MergingRenovation(GenerateId(), start, end, rooms,
                RenovationStatus.BOOKED, newRoomName, newRoomType);
            //TODO: Add add method in mergingrenovationRepo
            mergingRenovations.Add(mergingRenovation);
            Serialize();
        }

        public void StartMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            mergingRenovationController.Serialize();
            foreach (Room room in mergingRenovation.Rooms)
            {
                roomController.MoveEquipmentToStorage(room);
            }
            roomController.Serialize();
        }

        public void FinishMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            foreach (Room room in mergingRenovation.Rooms)
            {
                RemoveRoom(room);
            }
            Dictionary<Equipment, int> equipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(mergingRenovation.NewRoomName, mergingRenovation.NewRoomType, equipmentAmount);
            mergingRenovationController.MergingRenovations.Remove(mergingRenovation);
            mergingRenovationController.Serialize();
        }

        public void TryToExecuteMergingRenovations()
        {
            if (mergingRenovationController.MergingRenovations.Count > 0)
            {
                foreach (MergingRenovation mergingRenovation in mergingRenovationController.MergingRenovations)
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
