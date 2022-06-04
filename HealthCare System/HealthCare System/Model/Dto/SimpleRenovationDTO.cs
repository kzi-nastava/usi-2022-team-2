using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class SimpleRenovationDTO : Renovation
    {
        Room room;
        string newRoomName;
        TypeOfRoom newRoomType;

        public Room Room { get => room; set => room = value; }
        public string NewRoomName { get => newRoomName; set => newRoomName = value; }
        public TypeOfRoom NewRoomType { get => newRoomType; set => newRoomType = value; }

        public SimpleRenovationDTO(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status,
            Room room, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.room = room;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }
    }
}
