using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    public enum AppointmentState { WAITING, ACCEPTED, DENIED}
    class AppointmentRequest
    {
        int id;
        AppointmentState state;
        Patient patient;
        Appointment appointment;

        public AppointmentRequest() { }

        public AppointmentRequest(int id, AppointmentState state)
        {
            this.id = id;
            this.state = state;
        }

        public AppointmentRequest(int id, AppointmentState state, Patient pacijent, Appointment appointment)
        {
            this.id = id;
            this.state = state;
            this.patient = pacijent;
            this.appointment = appointment;
        }

        public AppointmentRequest(AppointmentRequest request)
        {
            this.id = request.id;
            this.state = request.state;
            this.patient = request.patient;
            this.appointment = request.appointment;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("state")]
        public AppointmentState State { get => state; set => state = value; }

        [JsonIgnore]
        internal Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        internal Appointment Appointment { get => appointment; set => appointment = value; }

        public override string ToString()
        {
            string patientInfo;
            if (this.patient is null) patientInfo = "null";
            else patientInfo = this.Patient.Jmbg;

            int appointmentInfo;
            if (this.appointment is null) appointmentInfo = -1;
            else appointmentInfo = this.appointment.Id;

            return "AppointmentRequest[" + "id: " + this.id.ToString() +
                ", state: " + this.state.ToString() + ", patient: " + patientInfo +
                ", appointment: " + appointmentInfo + "]";
        }
    }
}
