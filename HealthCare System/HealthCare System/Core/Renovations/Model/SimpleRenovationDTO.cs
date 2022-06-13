using HealthCare_System.Core.Rooms.Model;
using System;

namespace HealthCare_System.Core.Renovations.Model
{
    public class SimpleRenovationDto : Renovation
    {
        Room room;
        string newRoomName;
        TypeOfRoom newRoomType;

        public Room Room { get => room; set => room = value; }
        public string NewRoomName { get => newRoomName; set => newRoomName = value; }
        public TypeOfRoom NewRoomType { get => newRoomType; set => newRoomType = value; }

        public SimpleRenovationDto(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status,
            Room room, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.room = room;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }
    }
}
