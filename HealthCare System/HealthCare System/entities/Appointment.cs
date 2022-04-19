using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    public enum AppointmentType{ EXAMINATION, OPERATION }
    class Appointment
    {
        int id;
        DateTime start;
        DateTime end;

        Doctor doctor;
        Patient patient;

        AppointmentType type;

        public Appointment()
        {

        }

        public Appointment(int id, DateTime start, DateTime end, Doctor doctor, Patient patient, AppointmentType type)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.doctor = doctor;
            this.patient = patient;
            this.type = type;
        }

        public Appointment(int id, DateTime start, DateTime end, AppointmentType type)
        {
            this.start = start;
            this.end = end;
            this.type = type;
            this.doctor = null;
            this.patient = null;

        }

        public Appointment(Appointment appointment)
        {
            this.id = appointment.id;
            this.start = appointment.start;
            this.end = appointment.end;
            this.doctor = appointment.doctor;
            this.patient = appointment.patient;
            this.type = appointment.type;
        }

        [JsonPropertyName("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonPropertyName("start")]
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        [JsonPropertyName("end")]
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        [JsonIgnore]
        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }

        [JsonIgnore]
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        [JsonPropertyName("type")]
        public AppointmentType Type
        {
            get { return type; }
            set { type = value; }
        }

        public override string ToString()
        {
            string doctorInfo;
            if (this.doctor is null) doctorInfo = "null";
            else doctorInfo = this.Doctor.Jmbg;

            string patientInfo;
            if (this.patient is null) patientInfo = "null";
            else patientInfo = this.Patient.Jmbg;

            return "Appointment[" + "start: " + this.start.ToString("dd/MM/yyyy HH:mm") +
                ", end: " + this.end.ToString("dd/MM/yyyy HH:mm") + ", doctor: " + doctorInfo +
                ", patient: " + patientInfo + ", type: " + this.type.ToString() +"]";
        }
    }
}
