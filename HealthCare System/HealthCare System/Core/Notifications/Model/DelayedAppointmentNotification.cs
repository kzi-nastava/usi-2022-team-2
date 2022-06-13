using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Notifications.Model
{
    public class DelayedAppointmentNotification : Notification
    {
        Appointment appointment;
        bool seenByDoctor;
        bool seenByPatient;

        public DelayedAppointmentNotification() { }

        public DelayedAppointmentNotification(int id, string message) : base(id, message) 
        {
            this.seenByDoctor = false;
            this.seenByPatient = false;
        }

        public DelayedAppointmentNotification(int id, string message, bool seenByDoctor, bool seenByPatient) 
            : base(id, message)
        {
            this.seenByDoctor = false;
            this.seenByPatient = false;
        }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment)
            : base(id, message)
        {
            this.appointment = appointment;
            this.seenByDoctor = false;
            this.seenByPatient = false;
        }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment, bool seenByDoctor, bool seenByPatient)
            : base(id, message)
        {
            this.appointment = appointment;
            this.seenByDoctor = seenByDoctor;
            this.seenByPatient = seenByPatient;
        }

        public DelayedAppointmentNotification(DelayedAppointmentNotification notification)
            : base(notification.Id, notification.Message)
        {
            appointment = notification.Appointment;
            seenByDoctor = notification.SeenByDoctor;
            seenByPatient = notification.SeenByPatient;
        }

        [JsonPropertyName("seenByDoctor")]
        public bool SeenByDoctor { get => seenByDoctor; set => seenByDoctor = value; }

        [JsonPropertyName("seenByPatient")]
        public bool SeenByPatient { get => seenByPatient; set => seenByPatient = value; }

        [JsonIgnore]
        internal Appointment Appointment { get => appointment; set => appointment = value; }


        public override string ToString()
        {
            if (this.Message is null)
            {
                return "Appointment delayed.";
            }
            return this.Message;
        }
    }

}
