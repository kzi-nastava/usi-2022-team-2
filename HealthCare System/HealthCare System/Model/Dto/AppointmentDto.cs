using System;

namespace HealthCare_System.Model.Dto
{
    public class AppointmentDto
    {
        int id;
        DateTime start;
        DateTime end;
        Doctor doctor;
        Patient patient;
        Room room;
        AppointmentType type;
        AppointmentStatus status;
        Anamnesis anamnesis;
        bool graded;
        bool emergency;

        public AppointmentDto()
        {
        }

        public AppointmentDto(int id, DateTime start, DateTime end, Doctor doctor, Patient patient, Room room, 
            AppointmentType type, AppointmentStatus status, Anamnesis anamnesis, bool graded, bool emergency)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.doctor = doctor;
            this.patient = patient;
            this.room = room;
            this.type = type;
            this.status = status;
            this.anamnesis = anamnesis;
            this.graded = graded;
            this.emergency = emergency;
        }

        public int Id { get => id; set => id = value; }

        public DateTime Start { get => start; set => start = value; }

        public DateTime End { get => end; set => end = value; }

        public Doctor Doctor { get => doctor; set => doctor = value; }

        public Patient Patient { get => patient; set => patient = value; }

        public Room Room { get => room; set => room = value; }

        public AppointmentType Type { get => type; set => type = value; }

        public AppointmentStatus Status { get => status; set => status = value; }

        public Anamnesis Anamnesis { get => anamnesis; set => anamnesis = value; }

        public bool Graded { get => graded; set => graded = value; }

        public bool Emergency { get => emergency; set => emergency = value; }
    }
}
