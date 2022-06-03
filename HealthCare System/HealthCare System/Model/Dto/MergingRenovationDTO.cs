using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class MergingRenovationDTO : Renovation
    {
        List<Room> rooms;
        string newRoomName;
        TypeOfRoom newRoomType;

        public MergingRenovationDTO(int id, DateTime beginningDate, DateTime endingDate, List<Room> rooms,
            RenovationStatus status, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.rooms = rooms;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public List<Room> Rooms { get => rooms; set => rooms = value; }
        public string NewRoomName { get => newRoomName; set => newRoomName = value; }
        public TypeOfRoom NewRoomType { get => newRoomType; set => newRoomType = value; }
    }
}
