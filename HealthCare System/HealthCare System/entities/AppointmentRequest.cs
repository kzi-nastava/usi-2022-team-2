using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum AppointmentState { WAITING, ACCEPTED, DENIED}
    public enum RequestType { CREATE, UPDATE, DELETE}
    public class AppointmentRequest
    {
        int id;
        AppointmentState state;
        Patient patient;
        Appointment oldAppointment;
        Appointment newAppointment;
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

        public AppointmentRequest(int id, AppointmentState state, Patient pacijent, Appointment oldAppointment, Appointment newAppointment, RequestType type, DateTime requestCreated)
        {
            this.id = id;
            this.state = state;
            this.patient = pacijent;
            this.oldAppointment = oldAppointment;
            this.type = type;
            this.requestCreated = requestCreated;
        }

        public AppointmentRequest(AppointmentRequest request)
        {
            this.id = request.id;
            this.state = request.state;
            this.patient = request.patient;
            this.oldAppointment = request.oldAppointment;
            this.newAppointment = request.newAppointment;
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
        public Appointment OldAppointment { get => oldAppointment; set => oldAppointment = value; }

        [JsonIgnore]
        public Appointment NewAppointment { get => newAppointment; set => newAppointment = value; }

        [JsonPropertyName("type")]
        public RequestType Type { get => type; set => type = value; }

        [JsonPropertyName("requestCreated")]
        public DateTime RequestCreated { get => requestCreated; set => requestCreated = value; }

        public override string ToString()
        {
            string patientInfo;
            if (this.patient is null) patientInfo = "null";
            else patientInfo = this.Patient.Jmbg;

            int oldAppointmentInfo;
            if (this.oldAppointment is null) oldAppointmentInfo = -1;
            else oldAppointmentInfo = this.oldAppointment.Id;

            int newAppointmentInfo;
            if (this.newAppointment is null) newAppointmentInfo = -1;
            else newAppointmentInfo = this.newAppointment.Id;

            return "AppointmentRequest[" + "id: " + this.id.ToString() +
                ", state: " + this.state.ToString() + ", patient: " + patientInfo +
                ", appointment: " + oldAppointmentInfo + "]";
        }
    }
}
