using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class MergingRenovation : Renovation
    {
        List<Room> rooms;

        public MergingRenovation() { }

        public MergingRenovation(Renovation renovation) : base(renovation)
        {
            this.rooms = null;
        }

        public MergingRenovation(int id, DateTime beginningDate, DateTime endingDate) : base(id, beginningDate, endingDate)
        {
            this.rooms = null;
        }

        public MergingRenovation(int id, DateTime beginningDate, DateTime endingDate, List<Room> rooms) : base(id, beginningDate, endingDate)
        {
            this.rooms = rooms;
        }

        public MergingRenovation(MergingRenovation renovation) : base(renovation)
        {
            this.rooms = renovation.Rooms;
        }

        [JsonIgnore]
        public List<Room> Rooms { get => rooms; set => rooms = value; }

        public override string ToString()
        {
            return "MergingRenovation" + base.ToString();
        }
    }
}
