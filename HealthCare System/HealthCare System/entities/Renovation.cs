using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class Renovation
    {
        int id;
        DateTime beginningDate;
        DateTime endingDate;

        public Renovation() { }

        public Renovation(int id, DateTime beginningDate, DateTime endingDate)
        {
            this.id = id;
            this.beginningDate = beginningDate;
            this.endingDate = endingDate;
        }

        public Renovation(Renovation renovation) 
        {
            this.id = renovation.id;
            this.beginningDate = renovation.beginningDate;
            this.endingDate = renovation.endingDate;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("beginningDate")]
        public DateTime BeginningDate { get => beginningDate; set => beginningDate = value; }

        [JsonPropertyName("endingDate")]
        public DateTime EndingDate { get => endingDate; set => endingDate = value; }

        public override string ToString()
        {
            return "[" + "start: " + this.beginningDate.ToString("dd/MM/yyyy HH:mm") + " end: " + this.endingDate.ToString("dd/MM/yyyy HH:mm") + "]";
        }
    }
}
