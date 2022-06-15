using HealthCare_System.Core.Users.Model;
using System;

namespace HealthCare_System.Core.DaysOffRequests.Model
{
    public class DaysOffRequestDto
    {
        int id;
        DateTime start;
        DateTime end;
        String description;
        DaysOffRequestState state;
        bool urgent;
        Doctor doctor;

        public DaysOffRequestDto(int id, DateTime start, DateTime end, string description, DaysOffRequestState state, 
            bool urgent, Doctor doctor)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.description = description;
            this.state = state;
            this.urgent = urgent;
            this.doctor = doctor;
        }

        public int Id { get => id; set => id = value; }

        public DateTime Start { get => start; set => start = value; }

        public DateTime End { get => end; set => end = value; }

        public string Description { get => description; set => description = value; }

        public DaysOffRequestState State { get => state; set => state = value; }

        public bool Urgent { get => urgent; set => urgent = value; }

        public Doctor Doctor { get => doctor; set => doctor = value; }
    }
}
