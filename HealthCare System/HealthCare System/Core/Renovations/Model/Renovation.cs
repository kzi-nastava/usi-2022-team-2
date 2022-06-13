using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Renovations.Model
{
    public enum RenovationStatus
    {
        BOOKED,
        ACTIVE,
        FINISHED
    }

    public abstract class Renovation
    {
        int id;
        DateTime beginningDate;
        DateTime endingDate;
        RenovationStatus status;

        public Renovation() { }

        public Renovation(int id, DateTime beginningDate, DateTime endingDate, RenovationStatus status)
        {
            this.id = id;
            this.beginningDate = beginningDate;
            this.endingDate = endingDate;
            this.status = status;
        }

        public Renovation(Renovation renovation) 
        {
            id = renovation.id;
            beginningDate = renovation.beginningDate;
            endingDate = renovation.endingDate;
            status = renovation.status;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("beginningDate")]
        public DateTime BeginningDate { get => beginningDate; set => beginningDate = value; }

        [JsonPropertyName("endingDate")]
        public DateTime EndingDate { get => endingDate; set => endingDate = value; }

        [JsonPropertyName("status")]
        public RenovationStatus Status { get => status; set => status = value; }

        public override string ToString()
        {
            return "[" + "start: " + beginningDate.ToString("dd/MM/yyyy HH:mm") + " end: " 
                + endingDate.ToString("dd/MM/yyyy HH:mm") + "]";
        }
    }
}
