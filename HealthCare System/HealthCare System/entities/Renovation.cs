using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public abstract class Renovation
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
            id = renovation.id;
            beginningDate = renovation.beginningDate;
            endingDate = renovation.endingDate;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("beginningDate")]
        public DateTime BeginningDate { get => beginningDate; set => beginningDate = value; }

        [JsonPropertyName("endingDate")]
        public DateTime EndingDate { get => endingDate; set => endingDate = value; }

        public override string ToString()
        {
            return "[" + "start: " + beginningDate.ToString("dd/MM/yyyy HH:mm") + " end: " 
                + endingDate.ToString("dd/MM/yyyy HH:mm") + "]";
        }
    }
}
