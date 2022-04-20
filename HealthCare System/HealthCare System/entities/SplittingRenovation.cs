using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class SplittingRenovation : Renovation
    {
        Room room;

        public SplittingRenovation() { }

        public SplittingRenovation(Renovation renovation) : base(renovation)
        {
            this.room = null;
        }

        public SplittingRenovation(int id, DateTime beginningDate, DateTime endingDate) : base(id, beginningDate, endingDate)
        {
            this.room = null;
        }

        public SplittingRenovation(int id, DateTime beginningDate, DateTime endingDate, Room room) : base(id, beginningDate, endingDate)
        {
            this.room = room;
        }

        public SplittingRenovation(SplittingRenovation renovation) : base(renovation)
        {
            this.room = renovation.room;
        }

        [JsonIgnore]
        public Room Room { get => room; set => room = value; }

        public override string ToString()
        {
            return "SplittingRenovation" + base.ToString();
        }
    }
}
