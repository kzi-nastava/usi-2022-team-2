using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.RenovationService
{
    class SimpleRenovationService
    {
        public void BookRenovation(DateTime start, DateTime end, Room room,
            string newRoomName, TypeOfRoom newRoomType)
        {
            SimpleRenovation simpleRenovation = new SimpleRenovation(GenerateId(), start, end,
                RenovationStatus.BOOKED, room, newRoomName, newRoomType);
            simpleRenovations.Add(simpleRenovation);
            Serialize();
        }
    }
}
