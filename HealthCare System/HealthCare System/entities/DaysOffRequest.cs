using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum DaysOffRequestState { WAITING, ACCEPTED, DENIED}
    class DaysOffRequest
    {
        int id;
        DateTime start;
        DateTime end;
        String description;
        DaysOffRequestState state;
        bool urgent;
        Doctor doctor;

        public DaysOffRequest()
{}

        public DaysOffRequest(int id,DateTime start, DateTime end, string description, DaysOffRequestState state, bool urgent)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.description = description;
            this.state = state;
            this.urgent = urgent;
        }

        public DaysOffRequest(int id, DateTime start, DateTime end, string description, DaysOffRequestState state, bool urgent, Doctor doctor)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.description = description;
            this.state = state;
            this.urgent = urgent;
            this.doctor = doctor;
        }

        public DaysOffRequest(DaysOffRequest request)
        {
            this.id = request.id;
            this.start = request.start;
            this.end = request.end;
            this.description = request.description;
            this.state = request.state;
            this.urgent = request.urgent;
            this.doctor = request.doctor;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("start")]
        public DateTime Start { get => start; set => start = value; }

        [JsonPropertyName("end")]
        public DateTime End { get => end; set => end = value; }

        [JsonPropertyName("description")]
        public string Description { get => description; set => description = value; }

        [JsonPropertyName("daysOffRequest")]
        public DaysOffRequestState State { get => state; set => state = value; }

        [JsonPropertyName("urgent")]
        public bool Urgent { get => urgent; set => urgent = value; }

        [JsonIgnore]
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (this.doctor is null) doctorInfo = "null";
            else doctorInfo = this.Doctor.Jmbg;

            return "DaysOffRequest[id: " + this.id.ToString() + ", start: " + this.start.ToString("dd/MM/yyyy HH:mm") +
                ", end: " + this.end.ToString("dd/MM/yyyy HH:mm") + ", description: " + description +
                ", state: " + state.ToString() + ", urgent: " + urgent.ToString() + ", doctor: " + doctorInfo+ "]";
        }
    }
}
