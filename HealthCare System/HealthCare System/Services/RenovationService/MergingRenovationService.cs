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
    }
}
