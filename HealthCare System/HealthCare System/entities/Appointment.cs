using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    enum AppointmentType { EXAMINATION, OPERATION }

    enum AppointmentStatus { BOOKED, FINISHED }

    class Appointment
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

        public Appointment() { }

        public Appointment(int id, DateTime start, DateTime end, Doctor doctor, Patient patient, Room room, AppointmentType type, 
            AppointmentStatus status, Anamnesis anamnesis, bool graded, bool emergency)
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

        public Appointment(Appointment appointment)
        {
            id = appointment.id;
            start = appointment.start;
            end = appointment.end;
            doctor = appointment.doctor;
            patient = appointment.patient;
            room = appointment.room;
            type = appointment.type;
            status = appointment.status;
            anamnesis = appointment.anamnesis;
            graded = appointment.graded;
            emergency = appointment.emergency;
        }

        public Appointment(int id, DateTime start, DateTime end, AppointmentType type,
            AppointmentStatus status, bool graded, bool emergency)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.type = type;
            this.status = status;
            this.graded = graded;
            this.emergency = emergency;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("start")]
        public DateTime Start { get => start; set => start = value; }

        [JsonPropertyName("end")]
        public DateTime End { get => end; set => end = value; }

        [JsonPropertyName("graded")]
        public bool Graded { get => graded; set => graded = value; }

        [JsonPropertyName("emergent")]
        public bool Emergency { get => emergency; set => emergency = value; }

        [JsonIgnore]
        internal Doctor Doctor { get => doctor; set => doctor = value; }

        [JsonIgnore]
        internal Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        internal Room Room { get => room; set => room = value; }

        [JsonPropertyName("type")]
        internal AppointmentType Type { get => type; set => type = value; }

        [JsonPropertyName("status")]
        internal AppointmentStatus Status { get => status; set => status = value; }

        [JsonIgnore]
        internal Anamnesis Anamnesis { get => anamnesis; set => anamnesis = value; }


        public override string ToString()
        {
            string doctorInfo;
            if (doctor is null) doctorInfo = "-1";
            else doctorInfo = Doctor.Jmbg;

            string patientInfo;
            if (patient is null) patientInfo = "-1";
            else patientInfo = Patient.Jmbg;

            string anamnesisInfo;
            if (anamnesis is null) anamnesisInfo = "-1";
            else anamnesisInfo = Anamnesis.Description;

            string roomInfo = "";
            if (room is null) roomInfo = "-1";
            // else roomInfo = Room.Id.ToString();

            return "Appointment[" + "start: " + this.start.ToString("dd/MM/yyyy HH:mm") +
                ", end: " + end.ToString("dd/MM/yyyy HH:mm") + ", doctor: " + doctorInfo +
                ", patient: " + patientInfo + ", type: " + type.ToString() + ", anamnesis: " + anamnesisInfo + 
                ", room: " + roomInfo + ", emergancy: " + emergency + ", status: " + status.ToString() + ", graded: " + graded + "]";
        }
    }
}
