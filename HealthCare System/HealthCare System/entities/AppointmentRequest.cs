using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum AppointmentState { WAITING, ACCEPTED, DENIED}
    public enum RequestType { CREATE, UPDATE, DELETE}
    class AppointmentRequest
    {
        int id;
        AppointmentState state;
        Patient patient;
        Appointment appointment;
        RequestType type;
        DateTime requestCreated;

        public AppointmentRequest() { }

        public AppointmentRequest(int id, AppointmentState state, RequestType type, DateTime requestCreated)
        {
            this.id = id;
            this.state = state;
            this.type = type;
            this.requestCreated = requestCreated;
        }

        public AppointmentRequest(int id, AppointmentState state, Patient pacijent, Appointment appointment, RequestType type, DateTime requestCreated)
        {
            this.id = id;
            this.state = state;
            this.patient = pacijent;
            this.appointment = appointment;
            this.type = type;
            this.requestCreated = requestCreated;
        }

        public AppointmentRequest(AppointmentRequest request)
        {
            this.id = request.id;
            this.state = request.state;
            this.patient = request.patient;
            this.appointment = request.appointment;
            this.type = request.type;
            this.requestCreated = request.requestCreated;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("state")]
        public AppointmentState State { get => state; set => state = value; }

        [JsonIgnore]
        public Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        public Appointment Appointment { get => appointment; set => appointment = value; }

        [JsonPropertyName("type")]
        public RequestType Type { get => type; set => type = value; }

        [JsonPropertyName("requestCreated")]
        public DateTime RequestCreated { get => requestCreated; set => requestCreated = value; }

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
