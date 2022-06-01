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
    }
}
