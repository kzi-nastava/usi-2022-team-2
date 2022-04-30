using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DelayedAppointmentNotification : Notification
    {
        Appointment appointment;

        public DelayedAppointmentNotification() { }

        public DelayedAppointmentNotification(int id, string message) : base(id, message) { }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment)
            : base(id, message)
        {
            this.appointment = appointment;
        }

        public DelayedAppointmentNotification(DelayedAppointmentNotification notification)
            : base(notification.Id, notification.Message)
        {
            appointment = notification.Appointment;
        }

        [JsonIgnore]
        internal Appointment Appointment { get => appointment; set => appointment = value; }

        public override string ToString()
        {
            int appointmentInfo;
            if (appointment is null) appointmentInfo = 91;
            else appointmentInfo = appointment.Id;

            return "DrugNotification[" + "id: " + Id.ToString() +
                ", message: " + Message + ", appointment: " + appointmentInfo.ToString() + "]";
        }
    }

}
