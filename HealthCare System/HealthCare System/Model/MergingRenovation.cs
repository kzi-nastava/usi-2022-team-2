using HealthCare_System.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.Model
{
    public class MergingRenovation : Renovation
    {
        List<Room> rooms;
        string newRoomName;
        TypeOfRoom newRoomType;

        public MergingRenovation()
        {
            rooms = new List<Room>();
        }

        public MergingRenovation(Renovation renovation, string newRoomName, TypeOfRoom newRoomType)
            : base(renovation)
        {
            rooms = new List<Room>();
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public MergingRenovation(int id, DateTime beginningDate, DateTime endingDate, 
            RenovationStatus status, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            rooms = new List<Room>();
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public MergingRenovation(int id, DateTime beginningDate, DateTime endingDate, List<Room> rooms, 
            RenovationStatus status, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.rooms = rooms;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public MergingRenovation(MergingRenovation renovation) : base(renovation)
        {
            rooms = renovation.Rooms;
            newRoomName = renovation.newRoomName;
            newRoomType = renovation.newRoomType;
        }

        public MergingRenovation(MergingRenovationDTO renovationDTO) : base(renovationDTO)
        {
            rooms = renovationDTO.Rooms;
            newRoomName = renovationDTO.NewRoomName;
            newRoomType = renovationDTO.NewRoomType;
        }

        [JsonIgnore]
        public List<Room> Rooms { get => rooms; set => rooms = value; }

        [JsonPropertyName("newRoomName")]
        public string NewRoomName { get => newRoomName; set => newRoomName = value; }

        [JsonPropertyName("newRoomType")]
        public TypeOfRoom NewRoomType { get => newRoomType; set => newRoomType = value; }

        public override string ToString()
        {
            return "MergingRenovation" + base.ToString() + "NewRoomName : " + newRoomName;
        }
    }
}
