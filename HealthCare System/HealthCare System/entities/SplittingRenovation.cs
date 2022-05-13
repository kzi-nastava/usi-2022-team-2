using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class SplittingRenovation : Renovation
    {
        Room room;
        string firstNewRoomName;
        TypeOfRoom firstNewRoomType;
        string secondNewRoomName;
        TypeOfRoom secondNewRoomType;

        public SplittingRenovation() { }

        public SplittingRenovation(Renovation renovation, string firstNewRoomName, TypeOfRoom firstNewRoomType, 
            string secondNewRoomName, TypeOfRoom secondNewRoomType)
            : base(renovation)
        {
            room = null;
            this.firstNewRoomName = firstNewRoomName;
            this.firstNewRoomType = firstNewRoomType;
            this.secondNewRoomName = secondNewRoomName;
            this.secondNewRoomType = secondNewRoomType;
        }

        public SplittingRenovation(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status, 
            string firstNewRoomName, TypeOfRoom firstNewRoomType, string secondNewRoomName, TypeOfRoom secondNewRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            room = null;
            this.firstNewRoomName = firstNewRoomName;
            this.firstNewRoomType = firstNewRoomType;
            this.secondNewRoomName = secondNewRoomName;
            this.secondNewRoomType = secondNewRoomType;
        }

        public SplittingRenovation(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status, 
            Room room, string firstNewRoomName, TypeOfRoom firstNewRoomType, string secondNewRoomName, 
            TypeOfRoom secondNewRoomType)
            : base(id, beginningDate, endingDate, status)
        {
            this.room = room;
            this.firstNewRoomName = firstNewRoomName;
            this.firstNewRoomType = firstNewRoomType;
            this.secondNewRoomName = secondNewRoomName;
            this.secondNewRoomType = secondNewRoomType;
        }

        public SplittingRenovation(SplittingRenovation renovation) : base(renovation)
        {
            room = renovation.room;
            firstNewRoomName = renovation.firstNewRoomName;
            firstNewRoomType = renovation.firstNewRoomType;
            secondNewRoomName = renovation.secondNewRoomName;
            secondNewRoomType = renovation.secondNewRoomType;
        }

        [JsonIgnore]
        public Room Room { get => room; set => room = value; }

        [JsonPropertyName("firstNewRoomName")]
        public string FirstNewRoomName { get => firstNewRoomName; set => firstNewRoomName = value; }

        [JsonPropertyName("firstNewRoomType")]
        public TypeOfRoom FirstNewRoomType { get => firstNewRoomType; set => firstNewRoomType = value; }

        [JsonPropertyName("secondNewRoomName")]
        public string SecondNewRoomName { get => secondNewRoomName; set => secondNewRoomName = value; }

        [JsonPropertyName("secondNewRoomType")]
        public TypeOfRoom SecondNewRoomType { get => secondNewRoomType; set => secondNewRoomType = value; }

        public override string ToString()
        {
            return "SplittingRenovation" + base.ToString() + "NewRoomName : " + firstNewRoomName;
        }
    }
}
