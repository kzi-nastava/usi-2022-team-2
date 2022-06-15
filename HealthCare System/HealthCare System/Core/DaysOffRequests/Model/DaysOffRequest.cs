using HealthCare_System.Core.Users.Model;
using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.DaysOffRequests.Model
{
    public enum DaysOffRequestState { WAITING, ACCEPTED, DENIED}
    public class DaysOffRequest
    {
        int id;
        DateTime start;
        DateTime end;
        String description;
        DaysOffRequestState state;
        bool urgent;
        Doctor doctor;

        public DaysOffRequest() {}

        public DaysOffRequest(int id,DateTime start, DateTime end, string description,
            DaysOffRequestState state, bool urgent)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.description = description;
            this.state = state;
            this.urgent = urgent;
        }

        public DaysOffRequest(int id, DateTime start, DateTime end, string description,
            DaysOffRequestState state, bool urgent, Doctor doctor)
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
            id = request.id;
            start = request.start;
            end = request.end;
            description = request.description;
            state = request.state;
            urgent = request.urgent;
            doctor = request.doctor;
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
            if (doctor is null) doctorInfo = "null";
            else doctorInfo = Doctor.Jmbg;

            return "DaysOffRequest[id: " + id.ToString() + ", start: " + start.ToString("dd/MM/yyyy HH:mm") +
                ", end: " + end.ToString("dd/MM/yyyy HH:mm") + ", description: " + description +
                ", state: " + state.ToString() + ", urgent: " + urgent.ToString() + ", doctor: " + doctorInfo+ "]";
        }
    }
}
