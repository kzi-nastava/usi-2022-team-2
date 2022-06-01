using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.RenovationService
{
    class SplittingRenovationService
    {
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
