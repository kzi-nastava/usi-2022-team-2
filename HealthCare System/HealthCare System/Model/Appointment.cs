using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Model
{
    public enum AppointmentType { EXAMINATION, OPERATION }

    public enum AppointmentStatus {BOOKED, FINISHED, ON_HOLD }

    public class Appointment
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

        public Appointment(int id, DateTime start, DateTime end, Doctor doctor, Patient patient, 
            Room room, AppointmentType type, AppointmentStatus status, Anamnesis anamnesis, 
            bool graded, bool emergency)
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

        public static AppointmentType getTypeByDuration(int duration)
        {
            if (duration != 15) { return AppointmentType.OPERATION; }
            return AppointmentType.EXAMINATION;
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
        public Doctor Doctor { get => doctor; set => doctor = value; }

        [JsonIgnore]
        public Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        public Room Room { get => room; set => room = value; }

        [JsonPropertyName("type")]
        public AppointmentType Type { get => type; set => type = value; }

        [JsonPropertyName("status")]
        public AppointmentStatus Status { get => status; set => status = value; }

        [JsonIgnore]
        internal Anamnesis Anamnesis { get => anamnesis; set => anamnesis = value; }

        public void Validate()
        {
            if (patient is null)
                throw new Exception("Patient doesn't exist!");
            if (!doctor.IsAvailable(start, end))
                throw new Exception("Doctor is not available!");
            if (!patient.IsAvailable(start, end))
                throw new Exception("Patient is not available!");
            if (room is null)
                throw new Exception("Room is not found!");
        }

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
            else roomInfo = Room.Id.ToString();

            return "Appointment[id: " + id + ", start: " + start.ToString("dd/MM/yyyy HH:mm") +
                ", end: " + end.ToString("dd/MM/yyyy HH:mm") + ", doctor: " + doctorInfo +
                ", patient: " + patientInfo + ", type: " + type.ToString() + ", anamnesis: " 
                + anamnesisInfo + ", room: " + roomInfo + ", emergancy: " + emergency + ", status: "
                + status.ToString() + ", graded: " + graded + "]";
        }
    }
}
