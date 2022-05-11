using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class SimpleRenovation : Renovation
    {
        Room room;
        string newRoomName;
        TypeOfRoom newRoomType;

        public SimpleRenovation() { }

        public SimpleRenovation(Renovation renovation, string newRoomName, TypeOfRoom newRoomType) : base(renovation)
        {
            room = null;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public SimpleRenovation(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            room = null;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public SimpleRenovation(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status, Room room, string newRoomName, TypeOfRoom newRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.room = room;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
        }

        public SimpleRenovation(SimpleRenovation renovation) : base(renovation)
        {
            room = renovation.room;
            newRoomName = renovation.newRoomName;
            newRoomType = renovation.newRoomType;
        }

        [JsonIgnore]
        public Room Room { get => room; set => room = value; }

        [JsonPropertyName("newRoomName")]
        public string NewRoomName { get => newRoomName; set => newRoomName = value; }

        [JsonPropertyName("newRoomType")]
        public TypeOfRoom NewRoomType { get => newRoomType; set => newRoomType = value; }

        public override string ToString()
        {
            return "SimpleRenovation" + base.ToString() + "NewRoomName : " + newRoomName;
        }
    }
}
