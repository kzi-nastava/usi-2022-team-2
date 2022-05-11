using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DelayedAppointmentNotification : Notification
    {
        Appointment appointment;
        bool seen;

        public DelayedAppointmentNotification() { }

        public DelayedAppointmentNotification(int id, string message) : base(id, message) { this.seen = false; }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment)
            : base(id, message)
        {
            this.appointment = appointment;
            this.seen = false;
        }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment, bool seen)
            : base(id, message)
        {
            this.appointment = appointment;
            this.seen = seen;
        }

        public DelayedAppointmentNotification(DelayedAppointmentNotification notification)
            : base(notification.Id, notification.Message)
        {
            appointment = notification.Appointment;
            seen = notification.Seen;
        }

        [JsonPropertyName("seen")]
        public bool Seen { get => seen; set => seen = value; }

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
