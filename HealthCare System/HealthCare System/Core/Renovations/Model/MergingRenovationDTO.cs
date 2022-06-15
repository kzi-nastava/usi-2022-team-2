using HealthCare_System.Core.Rooms.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations.Model
{
    public class MergingRenovationDto : Renovation
    {
        List<Room> rooms;
        string newRoomName;
        TypeOfRoom newRoomType;

        public MergingRenovationDto(int id, DateTime beginningDate, DateTime endingDate, List<Room> rooms,
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
