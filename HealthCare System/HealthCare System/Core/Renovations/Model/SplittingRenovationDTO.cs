using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Renovations.Model
{
    public class SplittingRenovationDto : Renovation
    {
        Room room;
        string firstNewRoomName;
        TypeOfRoom firstNewRoomType;
        string secondNewRoomName;
        TypeOfRoom secondNewRoomType;

        public SplittingRenovationDto(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status,
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

        public Room Room { get => room; set => room = value; }
        public string FirstNewRoomName { get => firstNewRoomName; set => firstNewRoomName = value; }
        public TypeOfRoom FirstNewRoomType { get => firstNewRoomType; set => firstNewRoomType = value; }
        public string SecondNewRoomName { get => secondNewRoomName; set => secondNewRoomName = value; }
        public TypeOfRoom SecondNewRoomType { get => secondNewRoomType; set => secondNewRoomType = value; }


    }
}
