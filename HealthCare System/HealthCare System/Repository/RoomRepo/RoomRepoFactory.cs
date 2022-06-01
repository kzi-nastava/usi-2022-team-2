using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.RoomRepo
{
    class RoomRepoFactory
    {
        private RoomRepo roomRepo;

        public RoomRepo CreateRoomRepository()
        {
            if (roomRepo == null)
                roomRepo = new RoomRepo();

            return roomRepo;
        }
    }
}
